using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public Transform prefabProjectile;

    public Transform positionToShoot;
    internal Transform projectileParent;
    public float timeBetweenShoot = .05f;    

    private Coroutine _currentCorrotine; 
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            _currentCorrotine = StartCoroutine(StartShoot());
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            if (_currentCorrotine != null)
            {
                StopCoroutine(_currentCorrotine);
            }
        }
    }

    IEnumerator StartShoot()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(timeBetweenShoot);
        }
    }

    public void Shoot()
    {        
        var projectile = Instantiate(prefabProjectile, projectileParent);
        projectile.transform.position = positionToShoot.position;
        projectile.transform.rotation = positionToShoot.rotation;
    }
}
