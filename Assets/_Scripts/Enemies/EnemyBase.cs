using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Animation;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour, IDamageable
    {
        public float maxHp = 10f;

        [SerializeField]
        private float _currentHp;
        private Collider _collider;

        [Header("Spawn")]
        public bool useSpawnEase;
        public Ease  spawnEase = Ease.OutBack;
        public float spawnEaseDuration = 0.5f;
        public float timeToDestroy = 1.5f;

        [Header("Animation")]
        [SerializeField]
        private AnimationBase _animationBase;

        public FlashColor flashColor;
        public ParticleSystem particlesDamage;
        
        void Awake()
        {
            _animationBase = GetComponent<AnimationBase>();
            _collider = GetComponent<Collider>();
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

        public void OnDamage(float dmg)
        {
            _currentHp -= dmg;
            
            if (flashColor != null) flashColor.Flash();
            if (particlesDamage != null) particlesDamage.Emit(20);

            if (_currentHp <= 0) Kill();
        }
        public void Damage(float dmg)
        {
            OnDamage(dmg);
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
            transform.DOScale(-1, spawnEaseDuration + Random.Range(0.1f, 0.5f)).SetEase(spawnEase).From();
        }

        public void PlayAnimationByTrigger(AnimationType animationType)
        {
            _animationBase.PlayAnimationByTrigger(animationType);
        }

        #endregion
    }
}
