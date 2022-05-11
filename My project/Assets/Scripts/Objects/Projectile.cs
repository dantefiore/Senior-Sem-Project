using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed; //the speed of the projectile
    public Vector2 direction;   //the direction of the projectile
    public Rigidbody2D myRigidBody; //the rigid 

    // Start is called before the first frame update
    void Start()
    {
        //sets the rigid body
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 initialVel)
    {
        //launches the projectile
        myRigidBody.velocity = initialVel * speed;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        //if the projectile hits something it will destroy the projectile
        if (other.CompareTag("Enemy") || other.CompareTag("Collision"))
        {
            Destroy(this.gameObject);
        }
    }
}
