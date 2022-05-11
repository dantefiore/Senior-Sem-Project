using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : PowerUp
{
    public Inventory playerInv; //the player's inventory

    // Start is called before the first frame update
    void Start()
    {
        powerUpSignal.Raise();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        //if the player touches the coin, the coin is added to
        //the player's inventory, and destroys the object
        if (other.CompareTag("Player") && other.isTrigger)
        {
            playerInv.coins += 1;

            powerUpSignal.Raise();
            Destroy(this.gameObject);
        }
    }
}
