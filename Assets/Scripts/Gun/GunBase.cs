using System.Collections;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public Transform prefabProjectile;

    public Transform positionToShoot;
    internal Transform projectileParent;
    public float timeBetweenShoot = .3f;    

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
        var projectile = Instantiate(prefabProjectile, projectileParent);
        projectile.transform.position = positionToShoot.position;
        projectile.transform.rotation = positionToShoot.rotation;
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
