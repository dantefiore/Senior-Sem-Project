using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthReaction : MonoBehaviour
{
    public FloatValue playerHealth; //the players health
    public SignalSender healthSignal; //the signal that connects the health to the UI

    //increases health when the player uses a health potion
    public void UseHealthPotion(int increaseAmt)
    {
        playerHealth.RuntimeValue += increaseAmt;
        healthSignal.Raise();
    }
}
