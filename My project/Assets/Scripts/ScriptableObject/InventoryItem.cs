using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Items")]
public class InventoryItem : ScriptableObject
{
    public string itemName;
    public string itemDesc;
    public Sprite itemImg;
    public int numHeld;
    public bool usable;
    public bool unique;
    public UnityEvent evt;

    public void Use()
    {
        evt.Invoke();
    }

    public void DecreaseAmount(int decreaseAmt)
    {
        numHeld -= decreaseAmt;
        if(numHeld < 0)
        {
            numHeld = 0;
        }
    }
}
