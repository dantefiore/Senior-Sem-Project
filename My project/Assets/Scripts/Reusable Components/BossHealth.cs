using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : EnemyHealth
{
    public override void Update()
    {
        //if the boss is less than 0 health
        if (currHealth <= 0)
        {
            DeathEffect();  //makes the death effect
            makeLoot(); //spawns loot
            death_count.RuntimeValue += 1;  //increases the death count
            this.gameObject.SetActive(false);   //makes the object disappear

        }
    }
}
