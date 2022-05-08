using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    //item's text and image
    [Header("UI that Change")]
    [SerializeField] private TextMeshProUGUI itemNumText;
    [SerializeField] private Image itemImg;

    [Header("Variables of Items")]
    public InventoryItem thisItem;  //the item
    public InventoryManager thisManager;  //the inventory manager

    //setting up the inventory
    public void SetUp(InventoryItem newItem, InventoryManager newManager)
    {
        thisItem = newItem;
        thisManager = newManager;

        if (thisItem)
        {
            itemImg.sprite = thisItem.itemImg;
            itemNumText.text = "" + thisItem.numHeld;
        }
    }

    public void Clicked()
    {   //when the item is clicked the description and button appeared
        if (thisItem)
        {
            thisManager.SetUpDescAndBtn(thisItem.itemDesc, thisItem.usable, thisItem);
        }
    }
}
