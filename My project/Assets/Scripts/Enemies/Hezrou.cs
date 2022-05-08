using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hezrou : Goblin
{
    public Transform[] path;    //the path the enemy follows
    public int currPoint;   //the current spot the enemy is moving to
    public Transform currGoal;  //the position of the current point
    public float roundingDistance;  //how fair the enemy should move around it

    public override void CheckDistance()
    {
        //checks to see if the player is in range to chase
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
        }   //goes back to its current point if the player is not in range
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

    //changes the target position when the enemy reaches the current one
    public virtual void ChangeGoal()
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
