using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    [SerializeField] private float lifeTime;    //how long the object will last

    // Update is called once per frame
    void Update()
    {
        //counts down
        lifeTime -= Time.deltaTime;

        //if lifetim is at or lower than 0, the object is destroyed
        if (lifeTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
