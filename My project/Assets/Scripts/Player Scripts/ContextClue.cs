using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextClue : MonoBehaviour
{
    public GameObject contextClue;  //the bubble above the player
    public bool contextActive = false;  //if teh bubble is up

    public void ContextChange()
    {
        contextActive = !contextActive; //changes the bool

        if (contextActive)
        {
            //makes the bubble appear
            contextClue.SetActive(true);
        }
        else
        {
            //makes the bubble disappear
            contextClue.SetActive(false);
        }
    }
}
