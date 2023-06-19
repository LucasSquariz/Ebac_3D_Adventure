using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour
    {
        public float startLife = 10f;

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
    }
}

