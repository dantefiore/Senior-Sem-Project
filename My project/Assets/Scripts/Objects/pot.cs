using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pot : MonoBehaviour
{
    private Animator anim;
    public LootTable thisLoot;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void makeLoot()
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

    public void Smash()
    {
        anim.SetBool("smashed", true);
        StartCoroutine(breakCo());
    }

    IEnumerator breakCo()
    {
        yield return new WaitForSeconds(.3f);
        makeLoot();
        this.gameObject.SetActive(false);
    }
}
