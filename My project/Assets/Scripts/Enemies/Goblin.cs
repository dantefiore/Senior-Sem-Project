using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Enemy
{
    public Rigidbody2D myRigidBody;
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        currState = EnemyState.idle;
        myRigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();
    }

    public virtual void CheckDistance()
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

                ChangeState(EnemyState.walk);
                anim.SetBool("walking", true);
            }
        }
        else if ((Vector3.Distance(target.position, transform.position) > chaseRadius))
        {
            anim.SetBool("walking", false);
        }
    }
   
    public void ChangeAnim(Vector2 direction)
    {
        anim.SetFloat("moveX", direction.x);
        anim.SetFloat("moveY", direction.y);
    }

    public void ChangeState(EnemyState new_state)
    {
        if(currState != new_state)
        {
            currState = new_state;
        }
    }
}
