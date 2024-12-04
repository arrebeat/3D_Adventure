using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Animation;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour, IDamageable
    {
        public int maxHp = 10;

        [SerializeField]
        private int _currentHp;

        [Header("Spawn")]
        public Transform model;
        public bool useSpawnEase;
        public Ease  spawnEase = Ease.OutBack;
        public float spawnEaseDuration = 0.5f;
        public float timeToDestroy = 1.5f;

        public bool lookAtPlayer = false;

        private AnimationBase _animationBase;
        private Collider _collider;
        private FlashColor _flashColor;
        private ParticleSystem _particlesDamage;
        public PlayerController_Astronaut _player { get; private set; }
        
        void Awake()
        {
            _animationBase = GetComponent<AnimationBase>();
            _collider = GetComponent<Collider>();
            _flashColor = GetComponent<FlashColor>();
            _particlesDamage = GetComponentInChildren<ParticleSystem>();

            GameObject playerObject = GameObject.Find("Player");
            _player = playerObject.GetComponent<PlayerController_Astronaut>();
        }

        void Start()
        {
            Init();
        }

        protected virtual void Init()
        {
            ResetHp();
            if (useSpawnEase) SpawnAnimation();
        }

        protected void ResetHp()
        {
            _currentHp = maxHp;
        }

        public virtual void Update()
        {
            if (lookAtPlayer && !_player.isDead)
            {
                model.transform.LookAt(_player.transform.position);
            }
        }

        public void OnDamage(int dmg)
        {
            _currentHp -= dmg;
            
            if (_flashColor != null) _flashColor.Flash();
            if (_particlesDamage != null) _particlesDamage.Emit(20);

            if (_currentHp <= 0) Kill();
        }
        public void Damage(int dmg)
        {
            OnDamage(dmg);
        }
        public void Damage(int dmg, Vector3 dir)
        {
            OnDamage(dmg);
            transform.DOMove(transform.position - dir, .1f);
        }

        void OnCollisionEnter(Collision other)
        {
            PlayerController_Astronaut p = other.transform.GetComponent<PlayerController_Astronaut>();

            if (p != null)
            {
                p.healthBase.Damage(1);
            }
        }

        protected virtual void Kill()
        {
            OnKill();
        }

        protected virtual void OnKill() 
        {
            PlayAnimationByTrigger(AnimationType.Death);
            if (_collider != null) _collider.enabled = false;
            Destroy(gameObject, timeToDestroy);
        }

        #region ANIMATION
        private void SpawnAnimation()
        {
            model.transform.DOScale(-1, spawnEaseDuration + Random.Range(0.1f, 0.5f)).SetEase(spawnEase).From();
        }

        public void PlayAnimationByTrigger(AnimationType animationType)
        {
            _animationBase.PlayAnimationByTrigger(animationType);
        }


        #endregion
    }
}
