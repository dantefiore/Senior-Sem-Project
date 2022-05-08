using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityReaction : MonoBehaviour
{
    public FloatValue ability;  //which ability is selected
    public GenericAbility newAbility; //the ability the player selected
    public Image image; //the image the player chose for the new ability
    [SerializeField] private Sprite icon;   //the icon of the selected ability
    [SerializeField] private int abilityNum;    //the ability selected

    public void SetAbility()
    {
        // image.enabled = true;
        ability.RuntimeValue = abilityNum;
       // abilitySignal.Raise();
        //image.sprite = icon;
    }
}
