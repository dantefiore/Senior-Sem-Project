using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Items")]
public class InventoryItem : ScriptableObject
{
    public string itemName; //the name of the item
    public string itemDesc; //the description of the item
    public Sprite itemImg;  //the image of the item
    public int numHeld; //the number of that item the player has
    public bool usable; //if the item is usable
    public bool unique; //if the player can have multiple
    public bool ability;    //if the item is an ability
    public UnityEvent evt;

    public void Use()
    {
        evt.Invoke();
    }

    public void DecreaseAmount(int decreaseAmt)
    {
        //decreases the amount the player has,
        //and won't let it go below that amount
        numHeld -= decreaseAmt;
        if(numHeld < 0)
        {
            numHeld = 0;
        }
    }
}
