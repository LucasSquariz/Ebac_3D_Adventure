using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class ItemLayoutManager : MonoBehaviour
    {
        [SerializeField, BoxGroup("References")] public ItemLayout prefabLayout;
        [SerializeField, BoxGroup("References")] public Transform container;
        [SerializeField, BoxGroup("References")] public List<ItemLayout> itemLayouts;

        private void Start()
        {
            CreateItems();
        }

        private void CreateItems()
        {
            foreach(var setup in ItemManager.Instance.itemSetups)
            {
                var item = Instantiate(prefabLayout, container);
                item.Load(setup);
                itemLayouts.Add(item);
            }
        }
    }
}

