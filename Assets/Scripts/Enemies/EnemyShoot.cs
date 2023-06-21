using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyShoot : EnemyBase
    {
        public GunEnemy gunEnemy;

        protected override void Init()
        {
            base.Init();

            gunEnemy.StartShoot();
        }
    }
}

