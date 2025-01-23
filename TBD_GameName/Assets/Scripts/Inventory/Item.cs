using interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{

    public class Item : MonoBehaviour, ICollectable
    {

        [SerializeField] private ItemDataSO itemDataSO;
        private ItemData itemData;
        private InventoryManager inventoryManager;
        //private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            inventoryManager = FindObjectOfType<InventoryManager>();
            SetDataValues();
        }

        private void SetDataValues()
        {
            itemData = new ItemData(itemDataSO);
        }

        public void Collect()
        {
            inventoryManager.AddToInventory(itemDataSO, itemData);
        }
    }
}

