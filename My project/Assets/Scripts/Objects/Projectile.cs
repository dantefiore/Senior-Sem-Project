using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public Vector2 direction;
    public float lifeTime;
    private float lifeTimeSecs;
    public Rigidbody2D myRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        lifeTimeSecs = lifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        lifeTimeSecs -= Time.deltaTime;

        if(lifeTimeSecs <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void Launch(Vector2 initialVel)
    {
        myRigidBody.velocity = initialVel * speed;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag("Enemy"))
            Destroy(this.gameObject);
    }
}
