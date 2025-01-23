using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{

    public class ItemData
    {
        public ItemDataSO itemSO;
        public int pickupAmount;
        public int amountInInventory { get; private set; }


        public ItemData(ItemDataSO itemSO)
        {
            this.itemSO = itemSO;
            pickupAmount = itemSO.pickupAmount;
            amountInInventory = itemSO.defaultAmount;
        }

        public void SetInventoryAmount(int amount)
        {
            amountInInventory = Mathf.Max(0, amountInInventory + amount);
        }
    }
}

