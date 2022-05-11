using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinTextManager : MonoBehaviour
{
    public Inventory playerInv; //th eplayer's inventory
    public TextMeshProUGUI coinDisplay; //the text showing the amount of coins the player has

    private void Start()
    {
        //shows the amount of coins the player has
        coinDisplay.text = "" + playerInv.coins;
    }

    public void updateCoinCount()
    {
        //updates the text when the player gains coins
        coinDisplay.text = "" + playerInv.coins;
    }
}
