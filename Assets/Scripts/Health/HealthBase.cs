using Animation;
using DG.Tweening;
using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cloth;

public class HealthBase : MonoBehaviour, IDamageable
{
    [SerializeField, BoxGroup("References")] public AnimationBase _animationBase;
    [SerializeField, BoxGroup("References")] public FlashColor flashColor;
    [SerializeField, BoxGroup("References")] public ParticleSystem particleSystem;
    [SerializeField, BoxGroup("References")] public List<UIUpdater> uiUpdaters;

    [SerializeField, BoxGroup("Life config")] public float startLife = 10f;
    [SerializeField, BoxGroup("Life config")] public bool destroyOnKill = false;
    [SerializeField, BoxGroup("Life config")] public float damageMultiply = 1f;

    public float _currentLife;

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

    public void ResetLife()
    {
        _currentLife = startLife;
        UpdateUI();
    }

    protected virtual void Kill()
    {
        if(destroyOnKill) Destroy(this.gameObject, 3f);
        OnKill?.Invoke(this);
    }    

    public void OnDamagetaken(float d)
    {
        if (flashColor != null) flashColor.Flash();
        if (particleSystem != null) particleSystem.Emit(15);

        //transform.position -= transform.forward;

        _currentLife -= d * damageMultiply;

        if (_currentLife <= 0)
        {
            PlayAnimationByTrigger(AnimationType.DEATH);
            Kill();
        }
        UpdateUI();
        OnDamage?.Invoke(this);
    }

    private void UpdateUI()
    {
        if (uiUpdaters != null)
        {
            uiUpdaters.ForEach(i => i.UpdateValue((float)_currentLife / startLife));
        }
    }

    [Button]
    public void DamageTest()
    {
        OnDamagetaken(5);
    }

    public void Damage(float damage)
    {
        OnDamagetaken(damage);
    }

    public void Damage(float damage, Vector3 direction)
    {
        transform.DOMove(transform.position - direction, .1f);        
        OnDamagetaken(damage);
    }
    public void PlayAnimationByTrigger(AnimationType animationType)
    {
        _animationBase?.PlayAnimationByTrigger(animationType);
    }

    public void ChangeDamageTaken(float damage, float duration)
    {
        StartCoroutine(ChangeDamageTakenCoroutine(damageMultiply, duration));
    }

    IEnumerator ChangeDamageTakenCoroutine(float damageMult, float duration)
    {
        damageMultiply = damageMult;
        yield return new WaitForSeconds(duration);
        damageMultiply = 1;
    }
}
