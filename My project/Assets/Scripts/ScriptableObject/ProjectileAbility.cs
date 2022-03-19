using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Ability/Projectile Ability", fileName = "New Projectile Ability")]
public class ProjectileAbility : GenericAbility
{
    [SerializeField] private GameObject thisProjectile;

    public override void Ability(Vector2 playerPos, Vector2 playerDir, Animator anim = null, Rigidbody2D myRigidBody = null)
    {
         if (playerMagic.currMagic >= magicCost)
        {
            playerMagic.currMagic -= magicCost;

        }
        else
            return;

        float rotationDir = Mathf.Atan2(playerDir.y, playerDir.x) * Mathf.Rad2Deg;

        GameObject newProjectile = Instantiate(thisProjectile, playerPos, Quaternion.Euler(0f, 0f, rotationDir));

        GenericProjectile temp = newProjectile.GetComponent<GenericProjectile>();

        if (temp)
        {
            temp.SetUp(playerDir);
        }
    }
}
