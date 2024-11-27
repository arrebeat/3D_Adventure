using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArreTools.StateMachine;
using DG.Tweening;
using System.Linq;

namespace Boss
{
    public enum BossStates
    {
        Init,
        Idle,
        Walk,
        Attack,
        Death
    }

    public class BossBase : MonoBehaviour
    {
        [Header("Spawn")]
        public Transform model;
        public bool useSpawnEase;
        public Ease  spawnEase = Ease.OutBack;
        public float spawnEaseDuration = 0.5f;
        public float timeToDestroy = 1.5f;

        [Header("Patrol")]
        public Transform[] waypoints;
        public float patrolSpeed = 5f;
        public float minDistance = .5f;
        
        private StateMachine<BossStates> stateMachine;
        private Coroutine _coroutine;

        void Start()
        {
            Init();
        } 

        protected virtual void Init()
        {
            stateMachine = new StateMachine<BossStates>();
            stateMachine.Init();

            stateMachine.RegisterStates(BossStates.Init, new BossStateInit());
            stateMachine.RegisterStates(BossStates.Walk, new BossStateWalk());
        }

        #region STATE MACHINE
        public void SwitchState(BossStates state)
        {
            //Debug.Log(state + " PORRA");
            stateMachine.SwitchState(state, this);
        }
        
        #endregion

        #region ANIMATION
        public virtual void SpawnAnimation()
        {
            model.transform.DOScale(0, spawnEaseDuration + Random.Range(0.1f, 0.5f)).SetEase(spawnEase);
        }
        #endregion
    
        public void MoveToRandomPoint()
        {
            if (_coroutine != null) StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(MoveToRandomPointCoroutine(waypoints[Random.Range(0, waypoints.Length)]));
        }

        public IEnumerator MoveToRandomPointCoroutine(Transform t)
        {
            while (Vector3.Distance(transform.position, t.transform.position) > minDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, t.transform.position, Time.deltaTime * patrolSpeed);
                yield return new WaitForEndOfFrame();
            }
        }

        #region DEBUG
        [NaughtyAttributes.Button]
        private void SwitchStateInit()
        {
            //Debug.Log("INIT PORRA");
            SwitchState(BossStates.Init);
        }
        
        [NaughtyAttributes.Button]
        private void SwitchStateWalk()
        {
            //Debug.Log("WALK CRL");
            SwitchState(BossStates.Walk);
        }
        #endregion
    }
}
