using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinTextManager : MonoBehaviour
{
    public Inventory playerInv;
    public TextMeshProUGUI coinDisplay;

    public void updateCoinCount()
    {
        coinDisplay.text = "" + playerInv.coins;
    }
}
