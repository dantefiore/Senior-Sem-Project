using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicManager : MonoBehaviour
{
    public Slider magicBar;
    public Inventory playerInv;

    // Start is called before the first frame update
    void Start()
    {
        magicBar.maxValue = playerInv.maxMagic;
        magicBar.value = playerInv.maxMagic;
        playerInv.currMagic = playerInv.maxMagic;
    }

    public void IncreaseMagic()
    {
        magicBar.value += 1;
        playerInv.currMagic += 1;

        if (magicBar.value > magicBar.maxValue)
        {
            magicBar.value = magicBar.maxValue;
            playerInv.currMagic = playerInv.maxMagic;
        }
    }

    public void DecreaseMagic(float amount)
    {
        magicBar.value -= amount;
        playerInv.currMagic -=  amount;

        if(magicBar.value <= 0)
        {
            magicBar.value = 0;
            playerInv.currMagic = 0;
        }
    }
}
