using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(menuName = "ScriptableObject/Ability/Dash Ability", fileName = "Dash Ability")]
public class DashAbility : GenericAbility
{
    public float dashForce;

    public override void Ability(Vector2 playerPos, Vector2 playerDir, Animator anim = null, Rigidbody2D myRigidBody = null)
    {
        if (playerMagic.currMagic >= magicCost)
        {
            playerMagic.currMagic -= magicCost;
            //useMagic.Raise();
        }
        else
            return;

        if (myRigidBody)
        {
            Vector3 dashVector = myRigidBody.transform.position + (Vector3)playerDir.normalized * dashForce;
            myRigidBody.DOMove(dashVector, duration);
        }
    }
}
