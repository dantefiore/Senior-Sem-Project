using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    public FloatValue abilityNum;
    public GenericAbility currAbility;
    public PlayerMovement player;
    public float start_cooldown;
    public float cooldown;

    public GenericAbility dash;
    public GenericAbility arrow;
    public GenericAbility ice;

    void Update()
    {
        cooldown--;

        if (Input.GetButtonDown("ability") && player.currentState != PlayerState.attack && player.currentState != PlayerState.stagger)
        {
            if (abilityNum.RuntimeValue == 0)
            {
                currAbility = null;
            }
            else if (abilityNum.RuntimeValue == 1)
            {
                currAbility = arrow;
            }
            else if (abilityNum.RuntimeValue == 2)
            {
                currAbility = dash;
            }
            else if(abilityNum.RuntimeValue == 3)
            {
                currAbility = ice;
            }

            if (currAbility == dash && cooldown <= 0)
            {
                StartCoroutine(AbilityCo(currAbility.duration));
                cooldown = start_cooldown;
            }
            else if(currAbility != null && currAbility != dash)
            {
                StartCoroutine(AbilityCo(currAbility.duration));
            }

            //if(playerInv.checkForItem(waterMagic)) //if the player has this ability
            //    StartCoroutine(SecondAttackCo());   //starts the second attack function
        }
    }

    public IEnumerator AbilityCo(float duration)
    {
        player.currentState = PlayerState.ability;
        currAbility.Ability(transform.position, player.facingDir, player.anim, player.myRigidBody);
        yield return new WaitForSeconds(duration);
        player.currentState = PlayerState.idle;
    }
}
