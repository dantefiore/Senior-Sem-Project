using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaReaction : MonoBehaviour
{
    public float playerMana;
    public SignalSender manaSignal;

    public void UseManaPotion(int increaseAmt)
    {
        playerMana += increaseAmt;
        manaSignal.Raise();
    }
}
