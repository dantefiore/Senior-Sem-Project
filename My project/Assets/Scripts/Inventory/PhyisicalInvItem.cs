using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhyisicalInvItem : MonoBehaviour
{
    //the player's inventory and item slot
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private InventoryItem thisItem;

    private void OnTriggerEnter2D(Collider2D other)
    {   //when the player walks into the item it adds it
        //into the inventory and destroys the collectable
        if (other.gameObject.CompareTag("Player") && !other.isTrigger)
        {
            AddItem();
            Destroy(this.gameObject);
        }
    }

    void AddItem()
    {
        //adds the item into the inventory,
        //if the player already has that item,
        //it increases the amount the player is holding
        if (playerInventory && thisItem)
        {
            if (playerInventory.myInv.Contains(thisItem))
            {
                thisItem.numHeld+=1;
            }
            else
            {
                playerInventory.myInv.Add(thisItem);
                thisItem.numHeld+=1;
            }
        }
    }
}
