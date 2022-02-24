using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorType { key, enemy, button }

public class Door : Interactable
{
    [Header("Door Variables")]
    public DoorType thisDoorType;
    public bool open = false;
    public Inventory inv;
    public SpriteRenderer doorSprite;
    public BoxCollider2D physCollider;
    public PlayerInventory playerInv;
    public InventoryItem thisItem;

    private void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            if (playerInRange && thisDoorType == DoorType.key)
            {
                if (inv.numKeys > 0 && thisItem.numHeld > 0)
                { 
                    inv.numKeys--;
                    thisItem.DecreaseAmount(1);
                    Open();
                }
            }
        }
    }

    public void Open()
    {
        doorSprite.enabled = false;
        open = true;
        physCollider.enabled = false;
    }

    public void Close()
    {
        doorSprite.enabled = true;
        open = false;
        physCollider.enabled = true;
    }
}
