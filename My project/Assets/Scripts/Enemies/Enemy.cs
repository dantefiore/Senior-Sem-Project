using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//the different states of the enemy
public enum EnemyState { idle, walk, attack, stagger }

public abstract class Enemy : MonoBehaviour
{
    public EnemyState currState;    //the current state of the enemy
    public FloatValue maxHealth;    //the float value that holds the max health
    public float health;    //the current health of the enemy
    public string enemyName;    //the name of the enemy
    public int baseAttack;  //the melee attack of the enemy
    public float moveSpeed; //the movement speed of the enemy
    public GameObject deathEffect;  //the animation that plays when the enemy dies
    public Vector2 homePos; //the position it starts out in
    public LootTable thisLoot;  //the loot it drops when defeated
    public SignalSender deathSignal;    //the signal it sends when it is defeated
    public GenericHealth healthScript;  //the health

    private void Awake()
    {   //when the player is in range
        homePos = transform.position;
        health = maxHealth.initialVal;
    }

    private void OnEnable()
    {   //when the enemy is activated, the position is set
        transform.position = homePos;
    }

    public virtual void TakeDamage(float dmg)
    {
        //when the enemy takes damage, subtract the health
        health -= dmg;
         
        //if the health is less then or equal to 0
        if(healthScript.currHealth <= 0)
        {
            DeathEffect();  //make the death effect
            makeLoot(); //spawn loot
            deathSignal.Raise();    //raise the signal
            this.gameObject.SetActive(false);   //deactivate the enemy

        }
    }

    public void DeathEffect()
    {
        if(deathEffect != null)
        {
            //spawns the death effect and plays its animation
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 0.5f);
        }
    }

    public void makeLoot()
    {
        //choose random loot and spawn it
        if (thisLoot != null)
        {
            PowerUp curr = thisLoot.LootDrop();

            if(curr != null)
            {
                Instantiate(curr.gameObject, transform.position, Quaternion.identity);
            }
        }
    }

    public void Knock(Rigidbody2D myRigidBody, float knockTime)
    {
        StartCoroutine(KnockCo(myRigidBody, knockTime));
    }

    public virtual IEnumerator KnockCo(Rigidbody2D myRigidBody, float knockTime)
    {
        if (myRigidBody != null)
        {
            //pushes back the enemy
            yield return new WaitForSeconds(knockTime);
            myRigidBody.velocity = Vector2.zero;

            currState = EnemyState.idle;
            myRigidBody.velocity = Vector2.zero;
        }
    }
}
