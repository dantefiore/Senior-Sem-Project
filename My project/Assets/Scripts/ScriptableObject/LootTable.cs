using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Loot 
{
    public PowerUp thisLoot;
    public int lootChance;
}

[CreateAssetMenu]
public class LootTable : ScriptableObject
{
    public Loot[] loot;

    public PowerUp LootDrop()
    {
        float cumlativeProb = 0;
        float currProb = Random.Range(0, 100);

        for(int i = 0; i<loot.Length; i++)
        {
            cumlativeProb += loot[i].lootChance;
            if(currProb <= cumlativeProb)
            {
                return loot[i].thisLoot;
            }
        }

        return null;
    }
}
