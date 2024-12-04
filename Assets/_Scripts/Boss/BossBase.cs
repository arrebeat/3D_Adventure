using System;
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
        public Ease spawnEase = Ease.OutBack;
        public float spawnEaseDuration = 0.5f;
        public float timeToDestroy = 1.5f;


        [Header("Patrol")]
        public Transform[] waypoints;
        public float patrolSpeed = 5f;
        public float minDistance = .5f;

        [Header("Attack")]
        public int maxAttacks = 5;
        public float timeBetweenAttacks = 1f;
        
        public StateMachine<BossStates> stateMachine;
        public HealthBase healthBase;
        private Coroutine _coroutine;

        void OnValidate()
        {
            healthBase = GetComponent<HealthBase>();    
        }

        void Start()
        {
            Init();
        } 

        protected virtual void Init()
        {
            healthBase = GetComponent<HealthBase>();
            healthBase.OnKill += OnBossKill;

            stateMachine = new StateMachine<BossStates>();
            stateMachine.Init();

            stateMachine.RegisterStates(BossStates.Init, new BossStateInit());
            stateMachine.RegisterStates(BossStates.Walk, new BossStateWalk());
            stateMachine.RegisterStates(BossStates.Attack, new BossStateAttack());
            stateMachine.RegisterStates(BossStates.Death, new BossStateDeath());

            model.gameObject.SetActive(false);
            foreach (var waypoint in waypoints) waypoint.SetParent(null);
        }

        private void OnBossKill(HealthBase h)
        {
            SwitchState(BossStates.Death);
        }

        #region STATE MACHINE
        public void SwitchState(BossStates state)
        {
            //Debug.Log(state + " PORRA");
            stateMachine.SwitchState(state, this);
        }
        
        #endregion

        #region ANIMATION
        public virtual void SpawnAnimation(Action onSpawnFinish = null)
        {
            model.gameObject.SetActive(true);
            model.transform.DOScale(-1, spawnEaseDuration + UnityEngine.Random.Range(0.1f, 0.5f)).SetEase(spawnEase).From();
            onSpawnFinish?.Invoke();
        }
        #endregion
    
        #region MOVE
        public void MoveToRandomPoint(Action onArrive = null)
        {
            if (_coroutine != null) StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(MoveToRandomPointCoroutine(waypoints[UnityEngine.Random.Range(0, waypoints.Length)], onArrive));
        }

        public IEnumerator MoveToRandomPointCoroutine(Transform t, Action onArrive = null)
        {
            while (Vector3.Distance(transform.position, t.transform.position) > minDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, t.transform.position, Time.deltaTime * patrolSpeed);
                yield return new WaitForEndOfFrame();
            }

            onArrive?.Invoke();
        }
        #endregion

        #region ATTACK
        public void Attack(Action onAttackFinish = null)
        {
            if (_coroutine != null) StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(AttackCoroutine(onAttackFinish));
        }

        public IEnumerator AttackCoroutine(Action onAttackFinish = null)
        {
            int attacks = 0;
            while (attacks < maxAttacks)
            {
                attacks ++;
                model.transform.DOScale(1.5f, timeBetweenAttacks).From();
                yield return new WaitForSeconds(timeBetweenAttacks);
            }

            onAttackFinish?.Invoke();
        }
        #endregion

        #region DEBUG
        [NaughtyAttributes.Button]
        private void SwitchStateInit()
        {
            //Debug.Log("INIT");
            SwitchState(BossStates.Init);
        }
        
        [NaughtyAttributes.Button]
        private void SwitchStateWalk()
        {
            //Debug.Log("WALK");
            SwitchState(BossStates.Walk);
        }
       
        [NaughtyAttributes.Button]
        private void SwitchStateAttack()
        {
            //Debug.Log("ATTACK");
            SwitchState(BossStates.Attack);
        }
        
        [NaughtyAttributes.Button]
        private void SwitchStateDeath()
        {
            //Debug.Log("DEATH");
            SwitchState(BossStates.Death);
        }
        #endregion
    }
}
