using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Animation;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour
    {
        [SerializeField, BoxGroup("Enemy config")] public float startLife = 10f;

        [SerializeField, BoxGroup("Animation config")] public float startAnimationDuration = .2f;
        [SerializeField, BoxGroup("Animation config")] public Ease startAnimationEase = Ease.OutBack;
        [SerializeField, BoxGroup("Animation config")] public bool startWithBornAnimation = true;

        public AnimationBase _animationBase;
        [ShowNonSerializedField] private float _currentLife;

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
            if(startWithBornAnimation) BornAnimation();
        }

        protected virtual void Kill() 
        {
            OnKill();
        }

        protected virtual void OnKill() 
        {
            Destroy(this.gameObject, 3f);
            PlayAnimationByTrigger(AnimationType.DEATH);
        }

        public void OnDamagetaken(float d)
        {
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

        //Debug
        private void Update()
        {

            if(Input.GetKeyDown(KeyCode.K))
            {
                OnDamagetaken(5f);
            }
        }
    }
}

