using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : GenericHealth
{
    public GameObject deathEffect;  //the animation that plays when the enemy dies
    public LootTable thisLoot;  //the loot that spawns when the enemy is defeated

    public override void Update()
    {
        //cheacks if the health is below or equal to 0
        if (currHealth <= 0)
        {
            DeathEffect();  //plays the death effect
            makeLoot(); //spawns loot
            death_count.RuntimeValue += 1;  //increases the death counter
            this.gameObject.SetActive(false);   //deactivates the enemy
        }
    }

    public void DeathEffect()
    {
        //plays the death animation
        if (deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 0.5f);
        }
    }

    public void makeLoot()
    {
        //chooses a random loot drop and spawns it
        if (thisLoot != null)
        {
            PowerUp curr = thisLoot.LootDrop();

            if (curr != null)
            {
                Instantiate(curr.gameObject, transform.position, Quaternion.identity);
            }
        }
    }
}
