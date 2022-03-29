using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : GenericHealth
{
    [SerializeField] private SignalSender healthSignal;
    [SerializeField] private FloatValue currScene;

    private void Start()
    {
        maxHealth.RuntimeValue = maxHealth.initialVal;
    }

    public override void Damage(float amount)
    {
        base.Damage(amount);
        maxHealth.RuntimeValue = currHealth;
        healthSignal.Raise();
    }

    private void Update()
    {
        if(maxHealth.RuntimeValue <= 0)
        {
            currScene.RuntimeValue = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene("Game Over");
        }
    }
}
