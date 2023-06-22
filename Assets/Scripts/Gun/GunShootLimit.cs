using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShootLimit : GunBase
{
    public List<UIUpdater> uiGunUpdaters;

    public float maxShoots = 5f;
    public float timeToRecharge = 1f;

    private float _currentBullets;
    private bool _isRecharging = false;

    private void Start()
    {
        GetAllUIs();
    }

    protected override IEnumerator ShootCoroutine()
    {        
        if (_isRecharging) yield break;

        while (true)
        {
            if (_currentBullets < maxShoots)
            {
                Shoot();
                _currentBullets++;
                UpdateUI();
                CheckRecharge();
                yield return new WaitForSeconds(timeBetweenShoot);
            }
            else
            {
                yield break;
            }
        }
    }

    private void CheckRecharge()
    {
        if (_currentBullets >= maxShoots)
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
            uiGunUpdaters.ForEach(i => i.UpdateValue(time/timeToRecharge));
            yield return new WaitForEndOfFrame();
        }
        _currentBullets = 0;
        _isRecharging = false;
    }

    private void UpdateUI()
    {
        uiGunUpdaters.ForEach(i => i.UpdateValue(maxShoots, _currentBullets));
    }

    private void GetAllUIs()
    {
        uiGunUpdaters = GameObject.FindObjectsOfType<UIUpdater>().ToList();        
    }
}
