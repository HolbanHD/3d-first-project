using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject ItemUiPrefab;
    private InventoryManager inventoryManager;

    private void Awake()
    {
        inventoryManager = FindAnyObjectByType<InventoryManager>();
    }

    private void Start()
    {
        inventoryManager.AddListenerToEvent(UpdateUI);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            gameObject.GetComponentInParent<Canvas>().enabled = !gameObject.GetComponentInParent<Canvas>().isActiveAndEnabled;
            UpdateUI();

        }
    }

    private void ShowUi()
    {
        foreach(var item in inventoryManager.GetItemInventory().Keys)
        {
            var collectable = Instantiate(ItemUiPrefab, transform);
            collectable.GetComponentInChildren<ItemUI>().SetData(item, inventoryManager.getKeyAmount(item), item.info);
        }
    }

    private void CleanUi()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    public void UpdateUI()
    {
        CleanUi();
        ShowUi();
    }
}
