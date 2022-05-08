using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : Goblin
{
    public GameObject projectile;   //the projectile the enemy fires
    public float fireRate;  //the amount of time between each shot
    private float fireDelay;    //the delay of each shot
    public bool canFire = true; //if the enemy can fire

    [Header("I Frames")]
    public Color flashColor; //the color the player flashes when hit
    public Color regColor;  //the characters normal colors
    public float flashDur; //how long the flashing lasts
    public int numOfFlash;  //the number of flashes
    public Collider2D triggerCollider; //the players hurt box
    public SpriteRenderer mySprite; //the characters sprite

    private void Update()
    {
        //counts down the time to fire
        fireDelay -= Time.deltaTime;

        if(fireDelay <= 0)
        {
            canFire = true;
            fireDelay = fireRate;
        }
    }

    public override void CheckDistance()
    { //if the player is in firing range
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius
            && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            //facing towards the player
            Vector3 vectorToTarget = target.position - transform.position;
            float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg) + 90;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 10);

            //if the enemy is able to attack, it will fire
            if (currState == EnemyState.idle || currState == EnemyState.walk && currState != EnemyState.stagger)
            {
                if (canFire)
                {
                    Vector3 tempVector = target.transform.position - transform.position;
                    GameObject curr = Instantiate(projectile, transform.position, Quaternion.identity);
                    curr.GetComponent<Projectile>().Launch(tempVector);

                    canFire = false;

                    ChangeState(EnemyState.walk);
                    anim.SetBool("walking", true);
                }
            }
        }
        else if ((Vector3.Distance(target.position, transform.position) > chaseRadius))
        {
            anim.SetBool("walking", false);
        }
    }

    //if the boss has been hit
    public override IEnumerator KnockCo(Rigidbody2D myRigidBody, float knockTime)
    {
        if (myRigidBody != null)
        {   //flash the colors
            StartCoroutine(FlashCo());
        }

        return base.KnockCo(myRigidBody, knockTime);
    }

    private IEnumerator FlashCo()
    {
        //plays the flashing animations
        for (int i = 0; i < numOfFlash; i++)
        {
            //repeatedly change the colors
            mySprite.color = flashColor;
            yield return new WaitForSeconds(flashDur);
            mySprite.color = regColor;
            yield return new WaitForSeconds(flashDur);
        }

        //mySprite.color = regColor;
    }
}

