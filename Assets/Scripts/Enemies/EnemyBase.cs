using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour
    {
        public float startLife = 10f;

        [ShowNonSerializedField] private float _currentLife;

        [SerializeField, BoxGroup("Animation config")] public float startAnimationDuration = .2f;
        [SerializeField, BoxGroup("Animation config")] public Ease startAnimationEase = Ease.OutBack;
        [SerializeField, BoxGroup("Animation config")] public bool startWithBornAnimation = true;

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
            Destroy(this.gameObject);
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
        #endregion
    }
}

