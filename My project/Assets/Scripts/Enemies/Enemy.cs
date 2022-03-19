using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState { idle, walk, attack, stagger }

public abstract class Enemy : MonoBehaviour
{
    public EnemyState currState;
    public FloatValue maxHealth;
    public float health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;
    public GameObject deathEffect;
    public Vector2 homePos;
    public LootTable thisLoot;
    public SignalSender deathSignal;
    [SerializeField] private GenericHealth healthScript;

    private void Awake()
    {
        homePos = transform.position;
        health = maxHealth.initialVal;
    }

    private void OnEnable()
    {
        transform.position = homePos;
    }

    public virtual void TakeDamage(float dmg)
    {
        health -= dmg;

        if(healthScript.currHealth <= 0)
        {
            DeathEffect();
            makeLoot();
            deathSignal.Raise();
            this.gameObject.SetActive(false);

        }
    }

    private void DeathEffect()
    {
        if(deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 0.5f);
        }
    }

    private void makeLoot()
    {
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
            yield return new WaitForSeconds(knockTime);
            myRigidBody.velocity = Vector2.zero;

            currState = EnemyState.idle;
            myRigidBody.velocity = Vector2.zero;
        }
    }
}
