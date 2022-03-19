using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactDamage : MonoBehaviour
{
    [SerializeField] private string tag;
    [SerializeField] private int dmg;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(tag))
        {
            GenericHealth temp = other.gameObject.GetComponent<GenericHealth>();

            if (temp)
            {
                temp.Damage(dmg);
            }

            Destroy(this.gameObject);
        }
    }
}
