using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//the types of the doors
public enum DoorType { key, enemy, button }

public class Door : Interactable
{
    [Header("Door Variables")]
    public DoorType thisDoorType;   //the type of door
    public bool open = false;   //if the door is opened or not
    public Inventory inv;   //the player's inventory
    public SpriteRenderer doorSprite;   //the sprite of the door
    public BoxCollider2D physCollider;  //the physical collider
    public PlayerInventory playerInv;   //the player's inventory
    public InventoryItem thisItem;  //the item required to open the door

    private void Update()
    {
        //if the player is in range and hits the button to open the door
        if (Input.GetButtonDown("Submit"))
        {
            //if teh door is a key door, and the player has a key to open it
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
        doorSprite.enabled = false; //makes the door sprite disappear
        open = true;    //sets the door to open
        physCollider.enabled = false;   //disables the physical collider
    }

    public void Close()
    {
        doorSprite.enabled = true;  //makes the door sprite appear
        open = false;   //sets the door to close
        physCollider.enabled = true;    //enables the physical collider
    }
}
