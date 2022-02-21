using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : Goblin
{
    public GameObject projectile;
    public float fireRate;
    private float fireDelay;
    public bool canFire = true;

    private void Update()
    {
        fireDelay -= Time.deltaTime;

        if(fireDelay <= 0)
        {
            canFire = true;
            fireDelay = fireRate;
        }
    }

    public override void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius
            && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            if (currState == EnemyState.idle || currState == EnemyState.walk && currState != EnemyState.stagger)
            {
                if (canFire)
                {
                    Vector3 tempVector = target.transform.position - transform.position;
                    GameObject curr = Instantiate(projectile, transform.position, Quaternion.identity);
                    curr.GetComponent<Projectile>().Launch(tempVector);

                    canFire = false;

                    ChangeState(EnemyState.walk);
                    anim.SetBool("walking", true);
                }
            }
        }
        else if ((Vector3.Distance(target.position, transform.position) > chaseRadius))
        {
            anim.SetBool("walking", false);
        }
    }
}
