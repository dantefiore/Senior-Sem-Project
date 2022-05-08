using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : PowerUp
{
    public FloatValue playerHealth;
    public float amountToIncrease;
    public FloatValue currHeartContainers;
    [SerializeField] private HeartManager manager;

    private void Start()
    {
        //playerHealth.RuntimeValue = playerHealth.initialVal;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.isTrigger)
        {
            playerHealth.RuntimeValue += amountToIncrease;

            if(playerHealth.RuntimeValue > currHeartContainers.RuntimeValue * 2f)
            {
                playerHealth.RuntimeValue = currHeartContainers.RuntimeValue * 2f;
            }
            //powerUpSignal.Raise();

            manager.UpdateHearts();

            Destroy(this.gameObject);
        }
    }
}
