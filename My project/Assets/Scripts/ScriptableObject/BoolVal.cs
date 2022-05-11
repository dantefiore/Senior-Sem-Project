using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class BoolVal : ScriptableObject
{
    //the initial value
    public bool initialVal;
    
    //the values during the playthrough of the game
    public bool RuntimeValue;
}
