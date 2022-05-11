using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartContainer : PowerUp
{
    public FloatValue heartContainers;
    public FloatValue playerHealth;
    public bool collected;
    public BoolVal storeState;

    private void Start()
    {
        //if the extra heart was collected or not
        collected = storeState.RuntimeValue;
        if (collected)
        {
            //if it was then it is inactive
            this.gameObject.SetActive(false);
        }
        else  //if not then it is active
            this.gameObject.SetActive(true);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        //if the player collides with the heart container, it disappears,
        //raises the players max life, and changes the store state
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            heartContainers.RuntimeValue += 1;
            playerHealth.RuntimeValue = heartContainers.RuntimeValue * 2;
            powerUpSignal.Raise();
            storeState.RuntimeValue = true;
            Destroy(this.gameObject);
        }
    }
}
