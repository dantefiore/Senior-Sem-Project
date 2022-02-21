using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walkingNPC : Sign
{
    private Vector3 dirVector;
    private Transform myTranform;
    public float speed;
    private float setSpeed;
    private Rigidbody2D myRigidBody;
    private Animator anim;
    public Collider2D bounds;
    private bool canMove = true;

    public float minMoveTime;
    public float maxMoveTime;
    private float moveTimeSeconds;

    public float minWaitTime;
    public float maxWaitTime;
    private float waitTimeSecs;

    public bool isMove = false;

    // Start is called before the first frame update
    void Start()
    {
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
