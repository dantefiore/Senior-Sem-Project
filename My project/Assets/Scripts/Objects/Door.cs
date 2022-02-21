using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorType { key, enemy, button }

public class Door : Interactable
{
    [Header("Door Variables")]
    public DoorType thisDoorType;
    public bool open = false;
    public Inventory playerInv;
    public SpriteRenderer doorSprite;
    public BoxCollider2D physCollider;

    private void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            if (playerInRange && thisDoorType == DoorType.key)
            {
                if (playerInv.numKeys > 0)
                {
                    playerInv.numKeys--;
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
