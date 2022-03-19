using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityReaction : MonoBehaviour
{
    public FloatValue ability;
    //public SignalSender abilitySignal;
    public GenericAbility newAbility;
    public Image image;
    [SerializeField] private Sprite icon;
    [SerializeField] private int abilityNum;

    public void SetAbility()
    {
        // image.enabled = true;
        ability.RuntimeValue = abilityNum;
       // abilitySignal.Raise();
        //image.sprite = icon;
    }
}
