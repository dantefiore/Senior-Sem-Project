using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Knockback : MonoBehaviour
{
    [SerializeField] private float thrust;  //the amoount of knockback
    [SerializeField] private float knockTime;   //the amount of time of kncokback
    [SerializeField] private string knockTag;   //the tag of the object
    private Rigidbody2D hit;    //what is getting hit

    private void OnTriggerEnter2D(Collider2D other)
    {
        //compares if the tags match and then does knockback
        if (other.gameObject.CompareTag(knockTag) && other.isTrigger)
        {
            hit = other.GetComponentInParent<Rigidbody2D>();

            if (hit != null)
            {
                //moves the object back
                Vector3 difference = hit.transform.position - transform.position;
                difference = difference.normalized * thrust;
                hit.DOMove(hit.transform.position + difference, knockTime);

                //checks the tag and moves the object and changes the state
                if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("DoorEnemy") || other.gameObject.CompareTag("breakable") 
                    || other.gameObject.CompareTag("Boss") || other.gameObject.CompareTag("FortuneTellerGoblin") && other.isTrigger)
                {
                    hit.GetComponent<Enemy>().currState = EnemyState.stagger;
                    other.GetComponent<Enemy>().Knock(hit, knockTime);
                }

                //checks the tag and moves the object and changes the state
                if (other.gameObject.CompareTag("Player") && other.GetComponentInParent<PlayerMovement>().currentState != PlayerState.stagger)
                {
                    hit.GetComponentInParent<PlayerMovement>().currentState = PlayerState.stagger;
                    other.GetComponentInParent<PlayerMovement>().Knock(knockTime);
                }
            }
        }
    }
}
