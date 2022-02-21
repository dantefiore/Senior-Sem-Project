using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hezrou : Goblin
{
    public Transform[] path;
    public int currPoint;
    public Transform currGoal;
    public float roundingDistance;

    public override void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius
                    && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            if (currState == EnemyState.idle || currState == EnemyState.walk && currState != EnemyState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

                Vector2 faceDirection = Vector3.Normalize(temp - transform.position);
                ChangeAnim(faceDirection);
                myRigidBody.MovePosition(temp);

                //ChangeState(EnemyState.walk);
                anim.SetBool("walking", true);
            }
        }
        else if ((Vector3.Distance(target.position, transform.position) > chaseRadius))
        {
            if(Vector3.Distance(transform.position, path[currPoint].position) > roundingDistance)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, path[currPoint].position, moveSpeed * Time.deltaTime);

                Vector2 faceDirection = Vector3.Normalize(temp - transform.position);
                ChangeAnim(faceDirection);
                myRigidBody.MovePosition(temp);
            }
            else
            {
                ChangeGoal();
            }
        }

        anim.SetBool("walking", true);
    }

    public void ChangeGoal()
    {
        if(currPoint == path.Length - 1)
        {
            currPoint = 0;
            currGoal = path[0];
        }
        else
        {
            currPoint++;
            currGoal = path[currPoint];
        }
    }
}
