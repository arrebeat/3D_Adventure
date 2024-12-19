using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Items
{
    public class UIItemLayoutManager : MonoBehaviour
    {
        public ItemManager itemManager { get; private set; }
        public UIItemLayout prefabItemLayout;
        public Transform container;
        public Image keycodeHealthPack;

        public List<UIItemLayout> uiItemLayouts;

        void OnValidate()
        {
        }

        void Start()
        {
            itemManager = GameObject.Find("ItemManager").GetComponent<ItemManager>();
            CreateUIItems();
        }

        void Update()
        {
            CheckHealthPakcs();
        }

        public void CreateUIItems()
        {
            foreach (var setup in itemManager.itemSetups)
            {
                var itemLayout = Instantiate(prefabItemLayout, container);
                itemLayout.LoadSetup(setup);
                uiItemLayouts.Add(itemLayout);
            }
        }

        private void CheckHealthPakcs()
        {
            if (itemManager.HealthPackAmount() > 0) keycodeHealthPack.enabled = true;
            else keycodeHealthPack.enabled = false;

        }
    }

}
