using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Naga : Hezrou
{
    [SerializeField] private float speed;   //the speed of the enemy
    [SerializeField] private Transform facingTarget; //the direction the enemy faces

    private void LateUpdate()
    {
        facingTarget = currGoal;    //faces the enemy towards its current goal
        Vector3 vectorToTarget = currGoal.position - transform.position;  //moves the enemy to the current goal

        //changes the rotation to look at the player
        float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg) + 90;  
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
    }

    //changes the target goal to the next one
    public override void ChangeGoal()
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
