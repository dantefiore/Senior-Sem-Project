using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FortuneTellerRoom : OverWorldRoom
{
    [SerializeField] private BoolVal storyPoint;
    [SerializeField] private GameObject sceneTransition;

    private void Start()
    {
        sceneTransition.SetActive(false);
    }

    private void Update()
    {
        if (GameObject.FindGameObjectsWithTag("FortuneTellerGoblin").Length <= 0)
        {
            storyPoint.RuntimeValue = true;
            sceneTransition.SetActive(true);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            virtualCam.SetActive(true);

            if (storyPoint.RuntimeValue)
            {
                for (int i = 0; i < enemies.Length; i++)
                {
                    ChangeActivation(enemies[i], false);
                    enemies[i].currState = EnemyState.idle;
                    enemies[i].health = enemies[i].maxHealth.initialVal;
                }
            }
            else
            {
                for (int i = 0; i < enemies.Length; i++)
                {
                    ChangeActivation(enemies[i], true);
                    enemies[i].currState = EnemyState.idle;
                    enemies[i].health = enemies[i].maxHealth.initialVal;
                }
            }
            

            for (int i = 0; i < pots.Length; i++)
            {
                ChangeActivation(pots[i], true);
            }
        }
    }
}
