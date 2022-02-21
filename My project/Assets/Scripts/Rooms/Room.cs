using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Enemy[] enemies;
    public pot[] pots;
    public GameObject virtualCam;

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            virtualCam.SetActive(true);

            for(int i = 0; i < enemies.Length; i++)
            {
                ChangeActivation(enemies[i], true);
                enemies[i].currState = EnemyState.idle;
                enemies[i].health = enemies[i].maxHealth.initialVal;
            }

            for (int i = 0; i < pots.Length; i++)
            {
                ChangeActivation(pots[i], true);
            }
        }
    }

    public virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            virtualCam.SetActive(false);

            for (int i = 0; i < enemies.Length; i++)
            {
                ChangeActivation(enemies[i], false);
            }

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
