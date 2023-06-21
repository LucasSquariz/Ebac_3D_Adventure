using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunEnemy : GunBase
{   
    private Coroutine _currentCorrotine;

    public override void Shoot()
    {
        var projectile = Instantiate(prefabProjectile);
        positionToShoot = GameObject.FindWithTag("EnemyGun").transform;

        projectile.transform.position = positionToShoot.position;
        projectile.transform.rotation = positionToShoot.rotation;
        projectile.speed = speed;
        projectile.transform.parent = null;
    }

    protected override IEnumerator ShootCoroutine()
    {
        return base.ShootCoroutine();
    }

    public void StartShoot()
    {
        StopShoot();
        _currentCorrotine = StartCoroutine(ShootCoroutine());
    }

    public void StopShoot()
    {
        if (_currentCorrotine != null)
        {
            StopCoroutine(_currentCorrotine);
        }
    }
}
