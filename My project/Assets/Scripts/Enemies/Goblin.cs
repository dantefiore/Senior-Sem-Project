using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Enemy
{
    public Rigidbody2D myRigidBody; //the rigid body of the enemy
    public Transform target;    //the enemy's target
    public float chaseRadius;   //the target radius
    public float attackRadius;  //the attack range
    public Animator anim;   //the enemy's animation

    // Start is called before the first frame update
    void Start()
    {
        currState = EnemyState.idle;    //enemy state is idle
        myRigidBody = GetComponent<Rigidbody2D>();  //sets the rigid body
        anim = GetComponent<Animator>();    //the animator
        target = GameObject.FindWithTag("Player").transform;    //finds the player and makes it the target
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance(); //finds the distance to the player
    }

    public virtual void CheckDistance()
    {
        //finds the if the player is in range, moves to it, and attack when its in range
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
        }   //if the player is not in range the enemy stops moving
        else if ((Vector3.Distance(target.position, transform.position) > chaseRadius))
        {
            anim.SetBool("walking", false);
        }
    }
   
    //change the animation direction depending on how its moving
    public void ChangeAnim(Vector2 direction)
    {
        anim.SetFloat("moveX", direction.x);
        anim.SetFloat("moveY", direction.y);
    }

    //changes the state of the enemy depending on what it is doing
    public void ChangeState(EnemyState new_state)
    {
        if(currState != new_state)
        {
            currState = new_state;
        }
    }
}
