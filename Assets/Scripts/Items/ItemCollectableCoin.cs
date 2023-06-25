using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Itens;

public class ItemCollectableCoin : ItemCollectableBase
{
    public Collider2D coinCollider;
    protected override void OnCollect()
    {
        base.OnCollect();
        ItemManager.Instance.AddByType(ItemType.COIN);
        coinCollider.enabled = false;
    }
}