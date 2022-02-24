using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : GenericHealth
{
    [SerializeField] private SignalSender healthSignal;

    public override void Damage(float amount)
    {
        base.Damage(amount);
        maxHealth.RuntimeValue = currHealth;
        healthSignal.Raise();
    }
}
