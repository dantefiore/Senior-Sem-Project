using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class Inventory : ScriptableObject
{
    public Item currItem;
    public List<Item> items = new List<Item>();
    public int numKeys;
    public int coins;
    public float maxMagic = 10;
    public float currMagic;

    public void OnEnable()
    {
        currMagic = maxMagic;
    }

    public void ReduceMagic(float magicCost)
    {
        currMagic -= magicCost;
    }

    public bool checkForItem(Item item)
    {
        if (items.Contains(item))
        {
            return true;
        }

        return false;
    }

    public void AddItem(Item itemToAdd)
    {
        //is the item a key
        if (itemToAdd.isKey)
        {
            numKeys++;
        }
        else
        {
            if (!items.Contains(itemToAdd))
            {
                items.Add(itemToAdd);
            }
        }
    }
}
