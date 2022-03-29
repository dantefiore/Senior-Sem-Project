using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : EnemyHealth
{
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
}
