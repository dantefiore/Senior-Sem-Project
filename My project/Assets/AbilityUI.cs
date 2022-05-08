using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityUI : MonoBehaviour
{
    [SerializeField] private FloatValue abilityNum;

    [Header("Ability Pics")]
    [SerializeField] private GameObject water;
    [SerializeField] private GameObject dash;
    [SerializeField] private GameObject ice;

    // Update is called once per frame
    void Update()
    {
        if (abilityNum.RuntimeValue == 0)
        {
            water.SetActive(false);
            dash.SetActive(false);
            ice.SetActive(false);
        }
        else if (abilityNum.RuntimeValue == 1)
        {
            water.SetActive(true);
            dash.SetActive(false);
            ice.SetActive(false);
        }
        else if (abilityNum.RuntimeValue == 2)
        {
            water.SetActive(false);
            dash.SetActive(true);
            ice.SetActive(false);
        }
        else if (abilityNum.RuntimeValue == 3)
        {
            water.SetActive(false);
            dash.SetActive(false);
            ice.SetActive(true);
        }
    }
}
