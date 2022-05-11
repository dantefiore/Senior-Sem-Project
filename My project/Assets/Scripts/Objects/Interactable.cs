using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public SignalSender context;    //the signal sender
    public bool playerInRange;  //if the player is in range

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        //if the player is in range, it turns on the bubble above the players head
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            context.Raise();
            playerInRange = true;
        }
    }

    public virtual void OnTriggerExit2D(Collider2D other)
    {
        //turns of the bubble if the player is not in range
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            context.Raise();
            playerInRange = false;
        }
    }
}
