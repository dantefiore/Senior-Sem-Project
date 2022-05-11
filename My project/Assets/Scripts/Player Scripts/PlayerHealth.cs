using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : GenericHealth
{
    //connects the health to the ui
    [SerializeField] private SignalSender healthSignal;

    //the current scent he player is in
    [SerializeField] private FloatValue currScene;

    private void Start()
    {
        //sets the max health of the player
        maxHealth.RuntimeValue = maxHealth.initialVal;
    }

    public override void Damage(float amount)
    {
        //lowers the players health when they are damaged
        base.Damage(amount);
        maxHealth.RuntimeValue -= amount;
        healthSignal.Raise();
    }

    private void Update()
    {
        //keeps the inner health counter and the scriptable object the same
        currHealth = maxHealth.RuntimeValue;

        //if the player is at or less than 0 health, it opens the game over screen
        if(maxHealth.RuntimeValue <= 0)
        {
            currScene.RuntimeValue = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene("Game Over");
        }
    }
}
