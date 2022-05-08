using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaReaction : MonoBehaviour
{
    public float playerMana;    //the players mana
    public SignalSender manaSignal; //the mana signal

    public void UseManaPotion(int increaseAmt)
    {
        //increases the player's mana when the item is used
        playerMana += increaseAmt;
        manaSignal.Raise();
    }
}
