using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : GenericHealth
{
    [SerializeField] private SignalSender healthSignal;

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
        if(currHealth <= 0)
        {
            SceneManager.LoadScene("Game Over");
        }
    }
}
