using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Items")]
public class InventoryItem : ScriptableObject
{
    public string itemName;
    public string itemDesc;
    public Sprite itemImg;
    public int numHeld;
    public bool usable;
    public bool unique;

}
