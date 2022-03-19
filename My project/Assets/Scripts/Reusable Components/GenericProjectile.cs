using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericProjectile : MonoBehaviour
{
    [SerializeField] private Rigidbody2D myRigidBody;
    [SerializeField] private float speed;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    public void SetUp(Vector2 movementDir)
    {
        myRigidBody.velocity = movementDir.normalized * speed;
    }
}
