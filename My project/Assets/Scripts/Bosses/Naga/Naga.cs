using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Naga : Hezrou
{
    [SerializeField] private float speed;
    [SerializeField] private Transform facingTarget;

    private void LateUpdate()
    {
        facingTarget = currGoal;
        Vector3 vectorToTarget = currGoal.position - transform.position;
        float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg) + 90;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
    }

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
