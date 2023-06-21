using Animation;
using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
    [SerializeField, BoxGroup("Life config")] public float startLife = 10f;
    [SerializeField, BoxGroup("Life config")] public bool destroyOnKill = false;

    [ShowNonSerializedField] private float _currentLife;

    public Action<HealthBase> OnDamage;
    public Action<HealthBase> OnKill;

    private void Start()
    {
        init();
    }

    public void init()
    {
        ResetLife();
    }

    protected void ResetLife()
    {
        _currentLife = startLife;
    }

    protected virtual void Kill()
    {
        if(destroyOnKill) Destroy(this.gameObject, 3f);
        OnKill?.Invoke(this);
    }

    [Button]
    public void Damage()
    {
        OnDamagetaken(5);
    }

    public void OnDamagetaken(float d)
    {
        //transform.position -= transform.forward;
        _currentLife -= d;

        if (_currentLife <= 0)
        {
            Kill();
        }
        OnDamage?.Invoke(this);
    }
}
