using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GenericDmg : MonoBehaviour
{
    [SerializeField] private float dmg; //the amount of damage dealt
    [SerializeField] private string tag;    //the tag it can attack

    private void OnTriggerEnter2D(Collider2D other)
    {
        //the objects tag is the same as the tag variables
        if (other.gameObject.CompareTag(tag) && other.isTrigger)
        {
            //takes in the enemies health
            GenericHealth temp = other.GetComponent<GenericHealth>();

            //if the health is not null, it does damage
            if (temp)
            {
                temp.Damage(dmg);
            }
        }
    }
}
