using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed;
    public Rigidbody2D myRigidBody;

    public float lifeTime;
    private float lifeTimeCounter;

    // Start is called before the first frame update
    void Start()
    {
        lifeTimeCounter = lifeTime;
    }

    private void Update()
    {
        lifeTimeCounter -= Time.deltaTime;

        if(lifeTimeCounter <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void SetUp(Vector2 vel, Vector3 direction)
    {
        myRigidBody.velocity = vel.normalized * speed;
        transform.rotation = Quaternion.Euler(direction);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Collision"))
        {
            Destroy(this.gameObject);
        }
            
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Room>() != null) 
            Destroy(gameObject);
    }
}
