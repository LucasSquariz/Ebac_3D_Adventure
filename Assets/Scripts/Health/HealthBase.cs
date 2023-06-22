using Animation;
using DG.Tweening;
using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour, IDamageable
{
    [SerializeField, BoxGroup("References")] public AnimationBase _animationBase;
    [SerializeField, BoxGroup("References")] public FlashColor flashColor;
    [SerializeField, BoxGroup("References")] public ParticleSystem particleSystem;
    [SerializeField, BoxGroup("References")] public List<UIUpdater> uiUpdaters;

    [SerializeField, BoxGroup("Life config")] public float startLife = 10f;
    [SerializeField, BoxGroup("Life config")] public bool destroyOnKill = false;

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

    protected void ResetLife()
    {
        _currentLife = startLife;
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

        transform.position -= transform.forward;
        _currentLife -= d;

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
}
