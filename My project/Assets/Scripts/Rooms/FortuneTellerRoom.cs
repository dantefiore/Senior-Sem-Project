using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FortuneTellerRoom : OverWorldRoom
{
    //the flags for the story
    [SerializeField] private BoolVal storyPoint;

    //the scene transition object
    [SerializeField] private GameObject sceneTransition;

    private void Start()
    {
        //turns off the scene transition
        sceneTransition.SetActive(false);
    }

    private void Update()
    {
        //finds tag and sees if those enemies were defeated
        if (GameObject.FindGameObjectsWithTag("FortuneTellerGoblin").Length <= 0)
        {
            storyPoint.RuntimeValue = true;
            sceneTransition.SetActive(true);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        //if the player enters the area
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            virtualCam.SetActive(true);

            //if the flag was hit, the enemies wont appear
            if (storyPoint.RuntimeValue)
            {
                for (int i = 0; i < enemies.Length; i++)
                {
                    ChangeActivation(enemies[i], false);
                    enemies[i].currState = EnemyState.idle;
                    enemies[i].health = enemies[i].maxHealth.initialVal;
                }
            }
            else  //if it wasn't, then the enemies do appear
            {
                for (int i = 0; i < enemies.Length; i++)
                {
                    ChangeActivation(enemies[i], true);
                    enemies[i].currState = EnemyState.idle;
                    enemies[i].health = enemies[i].maxHealth.initialVal;
                }
            }
            
            //makes all pots appear
            for (int i = 0; i < pots.Length; i++)
            {
                ChangeActivation(pots[i], true);
            }
        }
    }
}
