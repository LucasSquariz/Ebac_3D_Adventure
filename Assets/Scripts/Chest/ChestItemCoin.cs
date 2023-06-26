using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;
using Items;

public class ChestItemCoin : ChestItemBase
{
    public GameObject coinObject;
    //public Transform coinSpawnPosition;
    public int coinNumber = 5;
    public Vector2 randomRange = new Vector2(-2f, 2f);
    public float tweenEndTime = .5f;
    
    private List<GameObject> _items = new List<GameObject>();
    public override void ShowItem()
    {
        base.ShowItem();
        CreateItems();
    }

    [Button]
    private void CreateItems()
    {
        for(int i = 0; i < coinNumber; i++)
        {
            var coin = Instantiate(coinObject);
            coin.transform.position = transform.position + Vector3.forward * Random.Range(randomRange.x, randomRange.y) + Vector3.right * Random.Range(randomRange.x, randomRange.y);
            coin.transform.DOScale(0, 1f).SetEase(Ease.OutBack).From();
            _items.Add(coin);
        }
    }

    [Button]
    public override void Collect()
    {
        base.Collect();
        foreach(var coin in _items)
        {
            coin.transform.DOMoveY(2f, tweenEndTime).SetRelative();
            coin.transform.DOScale(0, tweenEndTime/2).SetDelay(tweenEndTime / 2);
            ItemManager.Instance.AddByType(ItemType.COIN);
        }
    }
}
