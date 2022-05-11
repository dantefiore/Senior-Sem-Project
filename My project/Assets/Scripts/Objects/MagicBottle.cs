using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBottle : PowerUp
{
    public Inventory playerInv; //the player's inventory

    public void OnTriggerEnter2D(Collider2D other)
    {
        //if the player collides with the magic bottle,
        //it adds it to the inventory and destroys it
        if (other.gameObject.CompareTag("Player"))
        {
            playerInv.currMagic += 1;
            Destroy(this.gameObject);
        }
    }
}
