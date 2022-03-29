using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeholderEye : Enemy
{
    public GameObject laser;
    [SerializeField] private float shootTime;
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    public bool canFire;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(shoot());

        if (Vector3.Distance(target.position, transform.position) <= chaseRadius
            && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            if (currState == EnemyState.idle || currState == EnemyState.walk && currState != EnemyState.stagger)
            {
                if (canFire)
                {
                    Vector3 tempVector = target.transform.position - transform.position;
                    GameObject curr = Instantiate(laser, transform.position, Quaternion.identity);
                    curr.GetComponent<Projectile>().Launch(tempVector);

                    canFire = false;
                }
            }
        }
    }

   private IEnumerator shoot()
    {
        yield return new WaitForSeconds(shootTime);
        canFire = true;
    }
}
