using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBottle : PowerUp
{
    public Inventory playerInv;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInv.currMagic += 1;
            powerUpSignal.Raise();
            Destroy(this.gameObject);
        }
    }
}