using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBeholder : TurretEnemy
{
    [SerializeField] private float speed;

    public override void CheckDistance()
    {

        //if the player is in attacking range
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius
            && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            //finds the direction and angel to the target, then changes the rotaion to face the target
            Vector3 vectorToTarget = target.position - transform.position;
            float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg);
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 10);

            //if the enemy is in a state to fire
            if (currState == EnemyState.idle || currState == EnemyState.walk && currState != EnemyState.stagger)
            {
                //and it is ready to fire
                if (canFire)
                {
                    //spawn a projectile and moves it towards the target
                    Vector3 tempVector = target.transform.position - transform.position;
                    GameObject curr = Instantiate(projectile, transform.position, Quaternion.identity);
                    curr.GetComponent<Projectile>().Launch(tempVector);

                    //change the bool so it can't fire
                    canFire = false;

                    ChangeState(EnemyState.walk);
                    anim.SetBool("walking", true);
                }
            }
        }   //if the player is out of the attack range
        else if ((Vector3.Distance(target.position, transform.position) > chaseRadius))
        {
            anim.SetBool("walking", false);
        }
    }
}
