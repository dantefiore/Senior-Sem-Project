using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class VectorValue : ScriptableObject
{
    //the initial value of the object
    public Vector2 initialVal;

    //teh value of the object during playthrough
    public Vector2 defaultVal;
}