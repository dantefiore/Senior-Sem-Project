using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [Header("UI that Change")]
    [SerializeField] private TextMeshProUGUI itemNumText;
    [SerializeField] private Image itemImg;

    [Header("Variables of Items")]
    public InventoryItem thisItem;
    public InventoryManager thisManager;

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



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
