using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Final : TurretEnemy
{
    public Transform[] path;
    public int currPoint;
    public Transform currGoal;
    public float roundingDistance;

    //check to see if the player is in attacking range
    public override void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius
                    && Vector3.Distance(target.position, transform.position) > attackRadius)
        { 
            //if the enemy is in a state that it can fire
            if (currState == EnemyState.idle || currState == EnemyState.walk && currState != EnemyState.stagger)
            {
                //the fire cooldown is done
                if (canFire)
                {
                    //finds the direction and distance of the target, then fires the projectile
                    Vector3 tempVector = target.transform.position - transform.position;
                    GameObject curr = Instantiate(projectile, transform.position, Quaternion.identity);
                    curr.GetComponent<Projectile>().Launch(tempVector);

                    canFire = false;

                    ChangeState(EnemyState.walk);
                    anim.SetBool("walking", true);
                }
            }
        }

        //if the enemy is at the target position, it then goes to the next target position
        if (Vector3.Distance(transform.position, path[currPoint].position) > roundingDistance)
        {
            Vector3 temp = Vector3.MoveTowards(transform.position, path[currPoint].position, moveSpeed * Time.deltaTime);

            Vector2 faceDirection = Vector3.Normalize(temp - transform.position);
            ChangeAnim(faceDirection);
            myRigidBody.MovePosition(temp);
        }
        else
        {
            ChangeGoal();   //changes the target position
        }
        

        anim.SetBool("walking", true);
    }

    //changes the target position
    //changes the target position
    public virtual void ChangeGoal()
    {
        if (currPoint == path.Length - 1)
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
