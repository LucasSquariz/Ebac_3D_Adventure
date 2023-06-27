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

    private void OnValidate()
    {
        if(healthBase == null) healthBase = GetComponent<HealthBase>();
    }

    private void Start()
    {
        OnValidate();
        healthBase.OnDamage += OnDamage;
    }

    private void OnDamage(HealthBase h)
    {
        transform.DOShakeScale(shakeDuration, Vector3.up, shakeForce);
        DropCoins();
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
