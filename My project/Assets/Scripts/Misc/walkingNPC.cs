using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walkingNPC : Sign
{
    private Vector3 dirVector;  //the direction the npc is moving
    private Transform myTranform;   //the position of the npc
    public float speed; //the speed of the npc
    private float setSpeed;
    private Rigidbody2D myRigidBody; //rigid body of the npc
    private Animator anim;  //animator of the npc
    public Collider2D bounds;   //the bounds the npc walks in
    private bool canMove = true;    //the bool deciding if the npc can move

    public float minMoveTime;   //the minimum number of seconds the npc changes direction
    public float maxMoveTime;   //the maximum number of seconds the npc changes direction
    private float moveTimeSeconds;  //the amount of time the npc will change the direction

    public float minWaitTime;   //the minimum amount of time the npc will wait before it moves
    public float maxWaitTime;   //the maximum amount of time the npc will wait before it moves
    private float waitTimeSecs; //how long the npc will wait

    public bool isMove = false; //if the noc is moving

    // Start is called before the first frame update
    void Start()
    {
        //chooses a random number between the ranges
        moveTimeSeconds = Random.Range(minMoveTime, maxMoveTime);
        waitTimeSecs = Random.Range(minWaitTime, maxWaitTime);

        setSpeed = speed;
        myTranform = GetComponent<Transform>();
        myRigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ChangeDirection();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        if (canMove)
        {
            //decreases the time to move, and when it runs out it waits
            moveTimeSeconds -= Time.deltaTime;
            if(moveTimeSeconds <= 0)
            {
                moveTimeSeconds = Random.Range(minMoveTime, maxMoveTime);
                canMove = true;
                isMove = true;
                UpdateAnim();
            }

            if (!playerInRange)
                Move();
        }
        else
        {
            //decreases the wait time, and when it runs out it moves again
            waitTimeSecs -= Time.deltaTime;
            if(waitTimeSecs <= 0)
            {
                ChooseDiffDir();
                
                canMove = false;
                isMove = false;
                waitTimeSecs = Random.Range(minWaitTime, maxWaitTime);

            }
        }
    }

    private void ChooseDiffDir()
    {
        //chooses a random direction and moves that way
        Vector3 temp = dirVector;
        ChangeDirection();

        int loops = 0;
        while (temp == dirVector && loops < 100)
        {
            loops++;
            ChangeDirection();
        }
    }

    private void Move()
    {
        //moves the npc
        Vector3 temp = myTranform.position + dirVector * speed * Time.deltaTime;
        if (bounds.bounds.Contains(temp))
        {
            myRigidBody.MovePosition(temp);
        }
        else
            ChangeDirection();
    }

    void ChangeDirection()
    {
        int direction = Random.Range(0, 4);

        //decides the direction of the npc depending
        //on the random number chosen
        switch (direction) {
            case 0:
                dirVector = Vector3.right;
                break;
            case 1:
                dirVector = Vector3.up;
                break;
            case 2:
                dirVector = Vector3.left;
                break;
            case 3:
                dirVector = Vector3.down;
                break;
            default:
                break;
        }

        UpdateAnim();
    }

    void UpdateAnim()
    {
        //updates the animation depending on the direction
        anim.SetFloat("moveX", dirVector.x);
        anim.SetFloat("moveY", dirVector.y);
        anim.SetBool("canMove", isMove);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        ChooseDiffDir();
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        //if the player is in the talk range, the npc stops moving
        if (other.CompareTag("npc") && !other.isTrigger)
        {
            context.Raise();
            playerInRange = true;
            isMove = false;
            canMove = false;
            UpdateAnim();
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        //if the player exits the talk area, the npc moves again
        if (other.CompareTag("npc") && !other.isTrigger)
        {
            context.Raise();
            playerInRange = false;
            canMove = true;
            isMove = true;
            Move();
            UpdateAnim();
        }
    }
}
