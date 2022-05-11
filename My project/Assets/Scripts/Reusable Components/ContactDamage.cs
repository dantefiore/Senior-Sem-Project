using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactDamage : MonoBehaviour
{
    [SerializeField] private string tag;    //the tag on the object
    [SerializeField] private int dmg;   //the amount of damage being dealt

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if the objects tag matched the variable tag
        if (other.gameObject.CompareTag(tag))
        {
           //takes in the targets health
            GenericHealth temp = other.gameObject.GetComponent<GenericHealth>();

            //if teh health is not null, it deals damage
            if (temp)
            {
                temp.Damage(dmg);
            }

            Destroy(this.gameObject);   //destroys the object attacking
        }
    }
}
