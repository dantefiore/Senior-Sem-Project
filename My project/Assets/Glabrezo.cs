using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glabrezo : MonoBehaviour
{
    public float Health;
    [SerializeField] private GameObject Target;
    public bool canAttack;
    public float Dist; //Distance from player
    public float Speed;
    public float Step;
    private Vector3 addedDistance = new Vector3(50f, 50f, 0f);
    [SerializeField] private Transform homepos;

    // Start is called before the first frame update
    void Start()
    {
        canAttack = false;
        CheckDistance();
        AIBehaviour();

        //Health = 100f;
        //transform.LookAt(Target.transform);
    }

    private void Update()
    {
        CheckDistance();
        AIBehaviour();
    }

    public void CheckDistance()
    {
        Dist = Vector3.Distance(Target.transform.position, transform.position);
    }

    public void AIBehaviour()
    {
        Step = Speed * Time.deltaTime;
     
        if (!canAttack && Dist > 4)
        {
            MoveTowardsTarget(Target.transform);
        }
        if (Dist < 4)
        {
            canAttack = true;
        }
        if (canAttack)
        {
            AttackBehaviour();
        }
    }

    private void MoveTowardsTarget(Transform _target)
    {
        transform.position = Vector2.MoveTowards(transform.position, _target.position + addedDistance, Step);
    }

    private void AttackBehaviour()
    {
        transform.position = Vector2.MoveTowards(transform.position, Target.transform.position, Step * 3);
        Debug.Log("Attack Behaviour");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Room") && collision.isTrigger)
        {
            StartCoroutine(Stunned());
            MoveTowardsTarget(homepos);
        }
    }

    private IEnumerator Stunned()
    {
        yield return new WaitForSeconds(1.5f);
    }

    //make a box collider 2d for the room thats just alittle bit smaller than it so when the boss leaves it its stunned for a little then moves back into the center of the room
}
