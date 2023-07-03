using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using TMPro;
using NaughtyAttributes;

namespace Items
{
    public enum ItemType
    {
        COIN,
        LIFE_PACK
    }

    public class ItemManager : Singleton<ItemManager>
    {
        public List<ItemSetup> itemSetups;        
        
        public TextMeshProUGUI uiTextCoins;
        public TextMeshProUGUI uiTextArtifacts;

        private void UpdateUI()
        {
            //UIInGameManager.UpdateTextCoins(coins.ToString());
            //UIInGameManager.UpdateTextArtifacts(artifacts.ToString());
            // uiTextCoins.text = coins.ToString();
        }

        private void Start()
        {
            Reset();
            LoadItemsFromSave();
        }

        public void LoadItemsFromSave()
        {
            AddByType(ItemType.COIN, SaveManager.Instance.Setup.coins);
            AddByType(ItemType.LIFE_PACK, SaveManager.Instance.Setup.healthPacks);
        }

        private void Reset()
        {
            foreach (var item in itemSetups)
            {
                item.soInt.value = 0;
            }
        }

        public ItemSetup GetItemByType(ItemType itemType)
        {            
            return itemSetups.Find(i => i.itemType == itemType);
           
        }

        public void AddByType(ItemType itemType, int amount = 1)
        {
            if (amount < 0) return;
            itemSetups.Find(i => i.itemType == itemType).soInt.value += amount;           
            
        }

        public void RemoveByType(ItemType itemType, int amount = 1)
        {            
            var item = itemSetups.Find(i => i.itemType == itemType);
            item.soInt.value -= amount;                       
            if (item.soInt.value < 0) item.soInt.value = 0;
        }

        [Button]
        private void AddCoin()
        {
            AddByType(ItemType.COIN);
        }

        [Button]
        private void AddLifePack()
        {
            AddByType(ItemType.LIFE_PACK);
        }

    }

    [System.Serializable]
    public class ItemSetup
    {
        public ItemType itemType;
        public SOInt soInt;
        public Sprite icon;
    }
}
