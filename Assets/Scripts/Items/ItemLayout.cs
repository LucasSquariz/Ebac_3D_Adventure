using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Items
{
    public class ItemLayout : MonoBehaviour
    {
        public Image uiIcon;
        public TextMeshProUGUI uiValue;

        private ItemSetup _currItemSetup;

        public void Load(ItemSetup setup)
        {
            _currItemSetup = setup;
            UpdateUI();
        }

        private void UpdateUI()
        {
            uiIcon.sprite = _currItemSetup.icon;
        }

        private void Update()
        {
            uiValue.text = _currItemSetup.soInt.value.ToString();
        }
    }
}

