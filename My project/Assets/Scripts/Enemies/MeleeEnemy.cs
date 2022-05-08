using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Goblin
{
    public override void CheckDistance()
    {
        //checks if the player is in the chase or attack range
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius
                    && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            if (currState == EnemyState.idle || currState == EnemyState.walk && currState != EnemyState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

                Vector2 faceDirection = Vector3.Normalize(temp - transform.position);
                ChangeAnim(faceDirection);
                myRigidBody.MovePosition(temp);

                ChangeState(EnemyState.walk);
                anim.SetBool("walking", true);
            }
        }//if the player is out of range, the enemy stops moving
        else if ((Vector3.Distance(target.position, transform.position) > chaseRadius))
        {
            anim.SetBool("walking", false);
        }//if the player is in attacking range, the enemy attacks
        else if (Vector3.Distance(target.position, transform.position) <= chaseRadius
                   && Vector3.Distance(target.position, transform.position) <= attackRadius)
        {
            if (currState == EnemyState.idle || currState == EnemyState.walk && currState != EnemyState.stagger)
                StartCoroutine(AttakcCO());
        }
    }

    //attacks the player
    public IEnumerator AttakcCO()
    {
        //changes the state and plays the animation
        currState = EnemyState.attack;
        anim.SetBool("attacking", true);
        yield return new WaitForSeconds(0.5f);
        currState = EnemyState.walk;
        anim.SetBool("attacking", false);
    }
}
