using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{

    private ItemButtonManager itemButtonManager;
    private TMP_Text countText;
    private TMP_Text infoText;
    private Image itemImage;
    private string info;

    private void Awake()
    {
        itemButtonManager = GetComponentInParent<ItemButtonManager>();
        itemImage = GetComponent<Image>();
        countText = GetComponentInChildren<TMP_Text>();

    }

    public void SetData(ItemDataSO itemDataSO , int count , string infoText)
    {
        itemImage.sprite = itemDataSO.sprite;
        countText.text = count.ToString();
        info = infoText;
    }

    private void SetInfoText()
    {
        infoText = GameObject.Find("InfoText").GetComponent<TMP_Text>();

        if (infoText == null)
        {
            infoText.text = string.Empty;
        }
        else
        {
            infoText.text = info;
        }
    }

    private void ClearInfoText()
    {
        infoText.text= string.Empty;
    }
}
