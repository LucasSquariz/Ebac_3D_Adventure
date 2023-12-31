using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public ProjectileBase prefabProjectile;
    public List<UIUpdater> uiGunUpdaters;

    public Transform positionToShoot;    
    public float timeBetweenShoot = .3f;
    public float speed = 50f;

    private Coroutine _currentCorrotine;    

    protected virtual IEnumerator ShootCoroutine()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(timeBetweenShoot);
        }
    }

    public virtual void Shoot()
    {        
        var projectile = Instantiate(prefabProjectile);
        ShakeCamera.Instance.Shake();
        positionToShoot = GameObject.FindWithTag("GunPosition").transform;

        projectile.transform.position = positionToShoot.position;
        projectile.transform.rotation = positionToShoot.rotation;
        projectile.speed = speed;
        projectile.transform.parent = null;

        
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
