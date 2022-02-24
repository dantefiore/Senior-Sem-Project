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
        collected = storeState.RuntimeValue;
        if (collected)
        {
            this.gameObject.SetActive(false);
        }
        else
            this.gameObject.SetActive(true);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
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
