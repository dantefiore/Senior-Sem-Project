using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestrictedEnemy : MeleeEnemy
{
    public Collider2D boundry;  //the boundry if the enemy

    public override void CheckDistance()
    {   //if the is in range and inside the boundy
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius
                   && Vector3.Distance(target.position, transform.position) > attackRadius
                   && boundry.bounds.Contains(target.transform.position))
        {   //if the enemy ism in the state that it can attack from 
            if (currState == EnemyState.idle || currState == EnemyState.walk && currState != EnemyState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

                Vector2 faceDirection = Vector3.Normalize(temp - transform.position);
                ChangeAnim(faceDirection);
                myRigidBody.MovePosition(temp);

                ChangeState(EnemyState.walk);
                anim.SetBool("walking", true);
            }
        }   //moves back into the boundry if the player leaves it
        else if ((Vector3.Distance(target.position, transform.position) > chaseRadius) || !boundry.bounds.Contains(target.transform.position))
        {
            anim.SetBool("walking", false);
        }   //if the player is in attacking range, the enemy attacks
        else if (Vector3.Distance(target.position, transform.position) <= chaseRadius
                  && Vector3.Distance(target.position, transform.position) <= attackRadius)
        {
            if (currState == EnemyState.idle || currState == EnemyState.walk && currState != EnemyState.stagger)
                StartCoroutine(AttakcCO());
        }
    }
}
