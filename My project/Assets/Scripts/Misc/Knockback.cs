using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Knockback : MonoBehaviour
{
    [SerializeField] private float thrust;
    [SerializeField] private float knockTime;
    [SerializeField] private string knockTag;
    private Rigidbody2D hit;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(knockTag) && other.isTrigger)
        {
            hit = other.GetComponentInParent<Rigidbody2D>();

            if (hit != null)
            {
                Vector3 difference = hit.transform.position - transform.position;
                difference = difference.normalized * thrust;
                hit.DOMove(hit.transform.position + difference, knockTime);

                if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("DoorEnemy") || other.gameObject.CompareTag("Boss") || other.gameObject.CompareTag("FortuneTellerGoblin") && other.isTrigger)
                {
                    hit.GetComponent<Enemy>().currState = EnemyState.stagger;
                    other.GetComponent<Enemy>().Knock(hit, knockTime);
                }
                if (other.gameObject.CompareTag("Player") && other.GetComponentInParent<PlayerMovement>().currentState != PlayerState.stagger)
                {
                    hit.GetComponentInParent<PlayerMovement>().currentState = PlayerState.stagger;
                    other.GetComponentInParent<PlayerMovement>().Knock(knockTime);
                }
            }
        }
    }
}
