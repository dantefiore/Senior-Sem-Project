using UnityEngine;
using DG.Tweening;
using System.Collections;

[CreateAssetMenu(menuName = "ScriptableObject/Ability/Dash Ability", fileName = "Dash Ability")]
public class DashAbility : GenericAbility
{
    public float dashForce; //the distance the person dashes

    public override void Ability(Vector2 playerPos, Vector2 playerDir, Animator anim = null, Rigidbody2D myRigidBody = null)
    {
        //if the player's magic is higher than the cost of the ability
        if (playerMagic.currMagic >= magicCost)
        {
            //decrease the magic
            playerMagic.currMagic -= magicCost;
            //useMagic.Raise();
        }
        else  //otherwise, don't do anything
            return;

        if (myRigidBody)
        {
            //push the character in a direction they press
            Vector3 dashVector = myRigidBody.transform.position + (Vector3)playerDir.normalized * dashForce;
            myRigidBody.DOMove(dashVector, duration);
        }
    }
}
