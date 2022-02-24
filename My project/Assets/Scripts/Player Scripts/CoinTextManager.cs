using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinTextManager : MonoBehaviour
{
    public Inventory playerInv;
    public TextMeshProUGUI coinDisplay;

    private void Start()
    {
        coinDisplay.text = "" + playerInv.coins;
    }

    public void updateCoinCount()
    {
        coinDisplay.text = "" + playerInv.coins;
    }
}
