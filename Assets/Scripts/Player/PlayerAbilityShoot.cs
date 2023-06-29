using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityShoot : PlayerAbilityBase
{
    public List<GunBase> gunBases;
    public Transform gunPosition;
    public FlashColor _flashColor;

    private GunBase _currentGun;
    private int _currentGunIndex = 0;

    protected override void Init()
    {
        base.Init();

        CreateGun(0);
        inputs.Gameplay.Shoot.performed += ctx => StartShoot();
        inputs.Gameplay.Shoot.canceled += ctx => CancelShoot();

        inputs.Gameplay.Weapon1.performed += ctx => ChangeGun(0);
        inputs.Gameplay.Weapon2.performed += ctx => ChangeGun(1);
    }    

    private void CreateGun(int gunIndex)
    {        
        _currentGun = Instantiate(gunBases[gunIndex], gunPosition);
        _currentGun.transform.localPosition = gunPosition.transform.localPosition;
    }

    private void ChangeGun(int gunIndex)
    {               
        if (gunIndex != _currentGunIndex)
        {
            Destroy(_currentGun.gameObject);
            CreateGun(gunIndex);
            _currentGunIndex = gunIndex;
        }
    }

    private void StartShoot()
    {
        _currentGun.StartShoot();
        _flashColor?.Flash();
    }

    private void CancelShoot()
    {
        _currentGun.StopShoot();        
    }
}
