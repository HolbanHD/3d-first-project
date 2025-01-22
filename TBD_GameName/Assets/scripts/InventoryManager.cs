using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private Dictionary<ItemDataSO,ItemData> _itemInventory = new Dictionary<ItemDataSO,ItemData>();

    public void AddToInventory(ItemDataSO itemDataSO , ItemData itemData)
    {
        if (_itemInventory.ContainsKey(itemDataSO))
        {
            _itemInventory[itemDataSO].SetInventoryAmount(itemData.pickupAmount);
        }
        else
        {
            _itemInventory.Add(itemDataSO, itemData);
            itemData.SetInventoryAmount(itemData.pickupAmount);
            print("kkk");
        }
    }

    public Dictionary <ItemDataSO,ItemData> GetItemInventory() {  return _itemInventory; }

    public int getKeyAmount(ItemDataSO itemDataSO)
    {
        return _itemInventory[itemDataSO].amountInInventory;
    }
}
