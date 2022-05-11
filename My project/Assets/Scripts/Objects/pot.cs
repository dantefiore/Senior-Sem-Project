using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pot : MonoBehaviour
{
    private Animator anim;  //the animator if the pot
    public LootTable thisLoot;  //the loot inside the pot

    // Start is called before the first frame update
    void Start()
    {
        //sets the animator of the pot
        anim = GetComponent<Animator>();
    }

    private void makeLoot()
    {
        //chooses loot at random and spawns it
        if (thisLoot != null)
        {
            PowerUp curr = thisLoot.LootDrop();

            if (curr != null)
            {
                Instantiate(curr.gameObject, transform.position, Quaternion.identity);
            }
        }
    }

    public void Smash()
    {
        //if the pot takes damage, it plays the break animation
        anim.SetBool("smashed", true);
        StartCoroutine(breakCo());
    }

    IEnumerator breakCo()
    {
        //makes loot after the animation is done
        yield return new WaitForSeconds(.3f);
        makeLoot();
        this.gameObject.SetActive(false);
    }
}
