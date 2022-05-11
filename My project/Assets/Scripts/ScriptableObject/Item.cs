using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class Item : ScriptableObject
{
    public Sprite itemSprite;   //the item's sprite
    public string itemName; //teh name of the item
    public bool isKey;  //if the item is a key
}
