using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{

    [CreateAssetMenu(fileName = "ItemSO", menuName = "create new item")]
    public class ItemDataSO : ScriptableObject
    {
        public GameObject prefab;
        public Sprite sprite;
        public int pickupAmount;
        public int defaultAmount;

        public string info;
        public string craftInfo;
        public string itemName;
    }
}

