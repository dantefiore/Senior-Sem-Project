using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestrictedEnemy : MeleeEnemy
{
    public Collider2D boundry;

    public override void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius
                   && Vector3.Distance(target.position, transform.position) > attackRadius
                   && boundry.bounds.Contains(target.transform.position))
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
        }
        else if ((Vector3.Distance(target.position, transform.position) > chaseRadius) || !boundry.bounds.Contains(target.transform.position))
        {
            anim.SetBool("walking", false);
        }
        else if (Vector3.Distance(target.position, transform.position) <= chaseRadius
                  && Vector3.Distance(target.position, transform.position) <= attackRadius)
        {
            if (currState == EnemyState.idle || currState == EnemyState.walk && currState != EnemyState.stagger)
                StartCoroutine(AttakcCO());
        }
    }
}
