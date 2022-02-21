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

    // Start is called before the first frame update
    void Start()
    {
        MakeInventorySlots();
        SetTextAndBtn("", false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
