using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        //breaks the pot if the player attacks it
        if (other.CompareTag("breakable"))
        {
            other.GetComponent<pot>().Smash();
        }
    }
}
