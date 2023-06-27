using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;

public class DestructibleItemBase : MonoBehaviour
{
    [SerializeField, BoxGroup("References")] public HealthBase healthBase;
    [SerializeField, BoxGroup("References")] public GameObject coinPrefab;
    [SerializeField, BoxGroup("References")] public Transform dropPosition;

    [SerializeField, BoxGroup("Animation setup")] public float shakeDuration = .1f;
    [SerializeField, BoxGroup("Animation setup")] public int shakeForce = 5;

    [SerializeField, BoxGroup("Drop setup")] public int dropCoinsAmount = 2;

    private Tween _currTween;

    private void OnValidate()
    {
        if(healthBase == null) healthBase = GetComponent<HealthBase>();
    }

    private void Start()
    {
        OnValidate();
        healthBase.OnDamage += OnDamageTaken;
    }

    private void OnDamageTaken(HealthBase h)
    {
        _currTween = transform.DOShakeScale(shakeDuration, Vector3.up, shakeForce);
        if(h._currentLife <= 0)
        {
            Invoke(nameof(DropGroupOfCoins), 1f);            
        }
        //Invoke(nameof(ResetAnimation), .3f);
    }

    private void ResetAnimation()
    {
        _currTween.Kill();
    }

    [Button]
    private void DropCoins()
    {
        var coin = Instantiate(coinPrefab);
        coin.transform.position = dropPosition.position;
        coin.transform.DOScale(0, 1f).SetEase(Ease.OutBack).From();
    }

    [Button]
    private void DropGroupOfCoins()
    {
        StartCoroutine(DropGroupOfCoinsCoroutine());
    }

    IEnumerator DropGroupOfCoinsCoroutine()
    {
        for (int i = 0; i < dropCoinsAmount; i++)
        {
            DropCoins();
            yield return new WaitForSeconds(.1f);
        }
    }
}
