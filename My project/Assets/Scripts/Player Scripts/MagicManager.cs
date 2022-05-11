using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicManager : MonoBehaviour
{
    public Slider magicBar; //the magic bar
    public Inventory playerInv; //the player's inventory

    // Start is called before the first frame update
    void Start()
    {
        //sets up the magic bar at the start of the scene
        magicBar.maxValue = playerInv.maxMagic;
        magicBar.value = playerInv.maxMagic;
        playerInv.currMagic = playerInv.maxMagic;
    }

    void Update()
    {
        //updates the magic bar whenever the player loses or gains magic
        magicBar.value = playerInv.currMagic;
    }

    public void IncreaseMagic(float amount)
    {
        //increases the magic bar and updates the ui
        magicBar.value += amount;
        playerInv.currMagic += amount;

        if (magicBar.value > magicBar.maxValue)
        {
            magicBar.value = magicBar.maxValue;
            playerInv.currMagic = playerInv.maxMagic;
        }
    }

    public void DecreaseMagic(float amount)
    {
        //decreases the magic bar and updates the ui
        magicBar.value -= amount;
        playerInv.currMagic -=  amount;

        if(magicBar.value <= 0)
        {
            magicBar.value = 0;
            playerInv.currMagic = 0;
        }
    }
}
