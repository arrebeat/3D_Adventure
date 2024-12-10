using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Items
{
    public class UIItemLayout : MonoBehaviour
    {
        public Image uiIcon;
        public TextMeshProUGUI uiValue;
        
        private ItemSetup _currentSetup;

        void OnValidate()
        {
            //uiIcon = GetComponentInChildren<Image>();
            //uiValue = GetComponentInChildren<TextMeshProUGUI>();    
        }

        public void LoadSetup(ItemSetup setup)
        {
            _currentSetup = setup;
            uiIcon.sprite = _currentSetup.icon;
        }

        private void UpdateUI()
        {
            
        } 

        void Update()
        {
            uiValue.text = _currentSetup.soInt.value.ToString();    
        }
    }

}
