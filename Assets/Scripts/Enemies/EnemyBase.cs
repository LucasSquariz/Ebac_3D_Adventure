using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Animation;
using UnityEngine.Events;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour, IDamageable
    {
        [SerializeField, BoxGroup("References")] public AnimationBase _animationBase;
        [SerializeField, BoxGroup("References")] public Collider collider;
        [SerializeField, BoxGroup("References")] public FlashColor flashColor;
        [SerializeField, BoxGroup("References")] public ParticleSystem particleSystem;

        [SerializeField, BoxGroup("Enemy config")] public float startLife = 10f;
        [SerializeField, BoxGroup("Enemy config")] public bool lookAtPlayer = false;

        [SerializeField, BoxGroup("Animation config")] public float startAnimationDuration = .2f;
        [SerializeField, BoxGroup("Animation config")] public Ease startAnimationEase = Ease.OutBack;
        [SerializeField, BoxGroup("Animation config")] public bool startWithBornAnimation = true;

        [SerializeField, BoxGroup("Events")] public UnityEvent onKillEvent;

        [ShowNonSerializedField] private float _currentLife;
        [ShowNonSerializedField] private Player _player;


        private void Start()
        {
            Init();
        }

        protected void ResetLife()
        {
            _currentLife = startLife;
        }

        protected virtual void Init()
        {
            ResetLife();
            _player = FindObjectOfType<Player>();
            if (startWithBornAnimation) BornAnimation();
        }

        protected virtual void Kill() 
        {
            OnKill();
        }

        protected virtual void OnKill() 
        {
            if (collider != null) collider.enabled = false;
            Destroy(this.gameObject, 3f);
            PlayAnimationByTrigger(AnimationType.DEATH);
            Debug.Log("inimigo Morreu");
            onKillEvent?.Invoke();
        }

        public void OnDamagetaken(float d)
        {
            if (flashColor != null) flashColor.Flash();
            if (particleSystem != null) particleSystem.Emit(15);

            transform.position -= transform.forward;

            _currentLife -= d;

            if (_currentLife <= 0)
            {
                Kill();
            }
        }

        #region ANIMATIONS
        private void BornAnimation()
        {
            transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
        }

        public void PlayAnimationByTrigger(AnimationType animationType)
        {
            _animationBase.PlayAnimationByTrigger(animationType);
        }

        #endregion

        public void Damage(float damage)
        {
            OnDamagetaken(damage);
        }

        public void Damage(float damage, Vector3 direction)
        {
            transform.DOMove(transform.position - direction, .1f);
            OnDamagetaken(damage);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Player player = collision.transform.GetComponent<Player>();

            if (player != null)
            {
                player.healthBase.Damage(1f);
            }
        }
        
        public virtual void Update()
        {

            if(lookAtPlayer)
            {
                transform.LookAt(_player.transform.position);
            }
        }

    }
}

