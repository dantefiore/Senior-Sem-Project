using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed; //the speed of the arrow
    public Rigidbody2D myRigidBody; //the rigid body of the arrow

    public float lifeTime;  //how long it lasts before it disappears
    private float lifeTimeCounter;  //the timer that counts down

    // Start is called before the first frame update
    void Start()
    {   //sets the counter
        lifeTimeCounter = lifeTime;
    }

    private void Update()
    {
        //lowers the counter
        lifeTimeCounter -= Time.deltaTime;

        //when the counter is at or less than 0,
        //the arrow is destroyes
        if(lifeTimeCounter <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void SetUp(Vector2 vel, Vector3 direction)
    {
        //it launches the arrow at a certain speed,
        //and it faces the direction the launcher is facing
        myRigidBody.velocity = vel.normalized * speed;
        transform.rotation = Quaternion.Euler(direction);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        //when the projectile collides with something, it is destroyed
        if (other.CompareTag("Enemy") || other.CompareTag("Collision"))
        {
            Destroy(this.gameObject);
        }
            
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        //when the arrow left the room it was
        //launched in, it was destroyed
        if (other.GetComponent<Room>() != null) 
            Destroy(gameObject);
    }
}
