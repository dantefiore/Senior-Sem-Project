using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Enemy[] enemies; //enemies in the room
    public pot[] pots;  //the pots in the room
    public GameObject virtualCam;   //the camera

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        //if the player enters the room
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            //turn on the camera
            virtualCam.SetActive(true);

            //make all enemies spawn
            for(int i = 0; i < enemies.Length; i++)
            {
                ChangeActivation(enemies[i], true);
                enemies[i].currState = EnemyState.idle;
                enemies[i].health = enemies[i].maxHealth.initialVal;
            }

            //make all pots appear
            for (int i = 0; i < pots.Length; i++)
            {
                ChangeActivation(pots[i], true);
            }
        }
    }

    public virtual void OnTriggerExit2D(Collider2D other)
    {
        //when the player leaves the area
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            //turn off the camera
            virtualCam.SetActive(false);

            //despawn the enemies in the room
            for (int i = 0; i < enemies.Length; i++)
            {
                ChangeActivation(enemies[i], false);
            }

            //despawn the pots in the room
            for (int i = 0; i < pots.Length; i++)
            {
                ChangeActivation(pots[i], false);
            }
        }
    }

    public void ChangeActivation(Component comp, bool activation)
    {
        comp.gameObject.SetActive(activation);
    }
}
