using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : PowerUp
{
    public FloatValue playerHealth; //the player's health float value
    public float amountToIncrease;  //how much to increase the health
    public FloatValue currHeartContainers;  //the current amount of heart containers
    [SerializeField] private HeartManager manager;  //the health UI manager

    private void Start()
    {
        //playerHealth.RuntimeValue = playerHealth.initialVal;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        //if the player collides with the heart
        if (other.CompareTag("Player") && other.isTrigger)
        {
            //increase the health amount
            playerHealth.RuntimeValue += amountToIncrease;

            //if the health exeeds the max health, it sets it to max
            if(playerHealth.RuntimeValue > currHeartContainers.RuntimeValue * 2f)
            {
                playerHealth.RuntimeValue = currHeartContainers.RuntimeValue * 2f;
            }

            //updates the health ui
            manager.UpdateHearts();

            Destroy(this.gameObject);   //destroys the heart pick up
        }
    }
}
