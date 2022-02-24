using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GenericDmg : MonoBehaviour
{
    [SerializeField] private float dmg;
    [SerializeField] private string tag;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(tag) && other.isTrigger)
        {
            GenericHealth temp = other.GetComponent<GenericHealth>();

            if (temp)
            {
                temp.Damage(dmg);
            }
        }
    }
}
