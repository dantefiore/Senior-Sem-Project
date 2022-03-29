using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : GenericHealth
{
    public GameObject deathEffect;
    public LootTable thisLoot;

    public override void Update()
    {
        if (currHealth <= 0)
        {
            DeathEffect();
            makeLoot();
            death_count.RuntimeValue += 1;
            this.gameObject.SetActive(false);
        }
    }

    public void DeathEffect()
    {
        if (deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 0.5f);
        }
    }

    public void makeLoot()
    {
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
