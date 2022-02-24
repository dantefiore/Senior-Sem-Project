using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhyisicalInvItem : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private InventoryItem thisItem;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !other.isTrigger)
        {
            AddItem();
            Destroy(this.gameObject);
        }
    }

    void AddItem()
    {
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
