

using System.Collections;
using UnityEngine;

public class GunShootLimit : GunBase
{
    public float maxBullets = 5f;
    public float timeToRecharge = 1f;

    private float _currentBullets;
    private bool _isRecharging = false;

    protected override IEnumerator ShootCoroutine()
    {        
        if (_isRecharging) yield break;

        while (true)
        {
            if(_currentBullets < maxBullets)
            {
                Shoot();
                _currentBullets++;
                CheckRecharge();
                yield return new WaitForSeconds(timeBetweenShoot);
            }
        }
    }

    private void CheckRecharge()
    {
        if (_currentBullets >= maxBullets)
        {
            StopShoot();
            StartRecharge();
        }
    }

    private void StartRecharge()
    {
        _isRecharging = true;
        StartCoroutine(RechargeCoroutine());
    }

    IEnumerator RechargeCoroutine()
    {
        float time = 0;
        while (time < timeToRecharge)
        {
            time += Time.deltaTime;
            Debug.Log("Recharging" + time);
            yield return new WaitForEndOfFrame();
        }
        _currentBullets = 0;
        _isRecharging = false;
    }
}
