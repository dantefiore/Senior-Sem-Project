using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nagaHealth : BossHealth
{
    [SerializeField] private GameObject body;
    [SerializeField] private GameObject tail;

    public override void Update()
    {
        if (currHealth <= 0)
        {
            DeathEffect();
            makeLoot();
            death_count.RuntimeValue += 1;
            this.gameObject.SetActive(false);

            body.SetActive(false);
            GameObject body_effect = Instantiate(deathEffect, body.transform.position, Quaternion.identity);
            Destroy(body_effect, 0.5f);

            tail.SetActive(false);
            GameObject tail_effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(tail_effect, 0.5f);
        }
    }
}
