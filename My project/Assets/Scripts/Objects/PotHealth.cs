using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotHealth : GenericHealth
{
    [SerializeField] private Animator anim;
    public LootTable thisLoot;

    // Start is called before the first frame update
    void Start()
    {
        //sets the pots health and animator
        currHealth = maxHealth.RuntimeValue;
        anim = GetComponent<Animator>();
    }

    public override void Update()
    {
        //if the health is less than or equal to 0, it plays the animation
        if (currHealth <= 0)
        {
            death_count.RuntimeValue += 1;
            anim.SetBool("smashed", true);
            StartCoroutine(breakCo());
        }
    }

    IEnumerator breakCo()
    {
        //waits until the aniimation is done to spawn loot
        yield return new WaitForSeconds(.3f);
        makeLoot();
        this.gameObject.SetActive(false);
    }

    private void makeLoot()
    {
        //chooses random loot and spawns it
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
