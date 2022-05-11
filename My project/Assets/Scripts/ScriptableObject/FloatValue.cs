using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class FloatValue : ScriptableObject
{
    //the initial value of the object
    public float initialVal;

    //the value during playtime
    public float RuntimeValue;
}
