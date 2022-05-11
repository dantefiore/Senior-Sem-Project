using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class Inventory : ScriptableObject
{
    public Item currItem;   //the current item the player wants to use
    public List<Item> items = new List<Item>(); //all items in the inventory
    public int numKeys; //the number of keys the player has
    public int coins;   //the total number of coins the player has
    public float maxMagic = 10; //max amount of magic the player has
    public float currMagic; //the current amount of magic the player has

    public void OnEnable()
    {
        //sets the current magic left
        currMagic = maxMagic;
    }

    public void ReduceMagic(float magicCost)
    {
        //decreases magic with the magic cost
        currMagic -= magicCost;
    }

    public bool checkForItem(Item item)
    {
        //if the player has that item
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
