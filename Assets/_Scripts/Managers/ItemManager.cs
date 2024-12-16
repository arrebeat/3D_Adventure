using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

namespace Items
{
    public enum ItemType
    {
        COIN,
        HEALTHPACK
    }

    public class ItemManager : MonoBehaviour
    {
        public List<ItemSetup> itemSetups;
        public bool reset = true;

        public static ItemManager instance;

        [SerializeField] private int _coinAmount;
        public int CoinAmount() { return _coinAmount; }
        [SerializeField] private int _healthPackAmount;
        public int HealthPackAmount() { return _healthPackAmount; }
        

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
                Destroy(this.gameObject); 
        }

        void Start()
        {
            if (reset) Reset();
            LoadItemsFromSave();
        }

        private void LoadItemsFromSave()
        {
            AddItemByType(ItemType.COIN, SaveManager.instance.saveSetup.coinAmount);
            AddItemByType(ItemType.HEALTHPACK, SaveManager.instance.saveSetup.healthPackAmount);
        }

        void Update()
        {
            _coinAmount = itemSetups[0].soInt.value;
            _healthPackAmount = itemSetups[1].soInt.value;
        }

        public void Reset()
        {
            foreach (var i in itemSetups) i.soInt.value = 0;
        }

        public ItemSetup GetItemByType(ItemType itemType)
        {
            return itemSetups.Find(i => i.itemType == itemType);
        }
        
        public void AddItemByType(ItemType itemType, int amount = 1)
        {
            itemSetups.Find(i => i.itemType == itemType).soInt.value += amount;
        }

        public void RemoveItemByType(ItemType itemType, int amount = 1)
        {
            var item = itemSetups.Find(i => i.itemType == itemType);
            item.soInt.value -= amount;

            if (item.soInt.value < 0) item.soInt.value = 0;
        }

        #region DEBUG
        [NaughtyAttributes.Button]
        private void AddCoin()
        {
            AddItemByType(ItemType.COIN);
        }

        [NaughtyAttributes.Button]
        private void AddHealthPack()
        {
            AddItemByType(ItemType.HEALTHPACK);
        }

        [NaughtyAttributes.Button]
        private void RemoveCoin()
        {
            RemoveItemByType(ItemType.COIN);
        }

        [NaughtyAttributes.Button]
        private void RemoveHealthPack()
        {
            RemoveItemByType(ItemType.HEALTHPACK);
        }

        [NaughtyAttributes.Button]
        private void ResetButton()
        {
            Reset();
        }

        #endregion

    }

    [System.Serializable]
    public class ItemSetup
    {
        public ItemType itemType;
        public SOInt soInt;
        public Sprite icon;
    }
}
