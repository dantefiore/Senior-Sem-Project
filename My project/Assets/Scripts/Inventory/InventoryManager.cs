using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    [Header("Inventory Info")]
    public PlayerInventory playerInv;   //the players inventory
    [SerializeField] private GameObject blankSlot;  //the slot where an item will go
    [SerializeField] private GameObject invPanel;   //the panel all the items are in
    [SerializeField] private TextMeshProUGUI descText;  //the text that pops up when the player selects an item
    [SerializeField] private GameObject useBtn; //the button that pops up for the player to use it
    public InventoryItem currItem;  //the current item the player is using

    public void SetTextAndBtn(string desc, bool btnActive)
    {
        descText.text = desc;   //sets the inventory text to the item's description

        //the button appears if the item is a usable item
        if (btnActive)
        {
            useBtn.SetActive(true);
        }
        else
            useBtn.SetActive(false);
    }

    void MakeInventorySlots()
    {
        if (playerInv)
        {
            for(int i =0; i<playerInv.myInv.Count; i++)
            {
                if (playerInv.myInv[i].numHeld > 0)
                {
                    GameObject temp = Instantiate(blankSlot, invPanel.transform.position, Quaternion.identity);
                    temp.transform.SetParent(invPanel.transform);
                    temp.transform.localScale = new Vector3(1, 1, 1);
                    InventorySlot newSlot = temp.GetComponent<InventorySlot>();

                    if (newSlot)
                    {
                        newSlot.SetUp(playerInv.myInv[i], this);
                    }

                }
            }
        }
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        clearInventorySlots();  //clears the inventory when it is open
        MakeInventorySlots();   //make slot for each item
        SetTextAndBtn("", false);   //clear the text and the buttons
    }

    public void SetUpDescAndBtn(string newDesc, bool isActive, InventoryItem newItem)
    {
        currItem = newItem; //sets the current item
        descText.text = newDesc;    //sets the descriptions
        useBtn.SetActive(isActive); //sets the button if applicable
    }

    private void clearInventorySlots()
    {   //clears out the inventory when it is closed
        for(int i = 0; i < invPanel.transform.childCount; i++)
        {
            Destroy(invPanel.transform.GetChild(i).gameObject);
        }
    }

    public void UseBtnPress()
    {
        //if the item needs the use button it will appear
        if (currItem)
        {
            currItem.Use();
            clearInventorySlots();
            MakeInventorySlots();

            if(currItem.numHeld == 0)
                SetTextAndBtn("", false);
        }
    }
}
