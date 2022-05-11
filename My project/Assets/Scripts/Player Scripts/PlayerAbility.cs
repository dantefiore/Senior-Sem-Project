using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    public FloatValue abilityNum;   //what ability the player is using
    public GenericAbility currAbility;  //the current ability the player has
    public PlayerMovement player;   //the player
    public float start_cooldown;    //the cooldown of the ability
    public float cooldown;  //the cooldown timer  of the ability

    public GenericAbility dash; //the dash ability
    public GenericAbility arrow;    //the arrow ability
    public GenericAbility ice;  //the ice ability

    void Update()
    {
        cooldown--; //the cooldown decreasing

        //if the player hits the ability button, it does the ability of whatever the current ability is
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
        //sets the player state to the ability then returns them to idle when the ability is done
        player.currentState = PlayerState.ability;
        currAbility.Ability(transform.position, player.facingDir, player.anim, player.myRigidBody);
        yield return new WaitForSeconds(duration);
        player.currentState = PlayerState.idle;
    }
}
