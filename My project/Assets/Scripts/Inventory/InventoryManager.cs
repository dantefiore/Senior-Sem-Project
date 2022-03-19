using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    [Header("Inventory Info")]
    public PlayerInventory playerInv;
    [SerializeField] private GameObject blankSlot;
    [SerializeField] private GameObject invPanel;
    [SerializeField] private TextMeshProUGUI descText;
    [SerializeField] private GameObject useBtn;
    public InventoryItem currItem;

    public void SetTextAndBtn(string desc, bool btnActive)
    {
        descText.text = desc;

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
        clearInventorySlots();
        MakeInventorySlots();
        SetTextAndBtn("", false);
    }

    public void SetUpDescAndBtn(string newDesc, bool isActive, InventoryItem newItem)
    {
        currItem = newItem;
        descText.text = newDesc;
        useBtn.SetActive(isActive);
    }

    private void clearInventorySlots()
    {
        for(int i = 0; i < invPanel.transform.childCount; i++)
        {
            Destroy(invPanel.transform.GetChild(i).gameObject);
        }
    }

    public void UseBtnPress()
    {
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
