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
        currHealth = maxHealth.RuntimeValue;
        anim = GetComponent<Animator>();
    }

    public override void Update()
    {
        if (currHealth <= 0)
        {
            death_count.RuntimeValue += 1;
            anim.SetBool("smashed", true);
            StartCoroutine(breakCo());
        }
    }

    IEnumerator breakCo()
    {
        yield return new WaitForSeconds(.3f);
        makeLoot();
        this.gameObject.SetActive(false);
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
}
