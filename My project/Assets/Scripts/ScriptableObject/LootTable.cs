using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Loot 
{
    public PowerUp thisLoot;    //what the loot is
    public int lootChance;  //the chance of each item being dropped
}

[CreateAssetMenu]
public class LootTable : ScriptableObject
{
    public Loot[] loot; //the list of items

    public PowerUp LootDrop()
    {
        //finds a random number
        float cumlativeProb = 0;
        float currProb = Random.Range(0, 100);

        for(int i = 0; i<loot.Length; i++)
        {
            //chooses the item depending on the random number, and spawns it
            cumlativeProb += loot[i].lootChance;
            if(currProb <= cumlativeProb)
            {
                return loot[i].thisLoot;
            }
        }

        return null;
    }
}
