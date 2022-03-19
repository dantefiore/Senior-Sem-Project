using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Ability/MultiShot Ability", fileName = "New MultiShot Ability")]
public class MultiShotAbility : GenericAbility
{
    [SerializeField] private GameObject thisProjectile;
    [SerializeField] private int numProj;
    [SerializeField] private float spread;

    public override void Ability(Vector2 playerPos, Vector2 playerDir, Animator anim = null, Rigidbody2D myRigidBody = null)
    {
        if (playerMagic.currMagic >= magicCost)
        {
            playerMagic.currMagic -= magicCost;

        }
        else
            return;

        float rotationDir = Mathf.Atan2(playerDir.y, playerDir.x) * Mathf.Rad2Deg;
        float startRotaion = rotationDir - spread / 2f;
        float angleIncrease = spread / ((float)numProj - 1f);

        for(int i =0; i < numProj; i++)
        {
            float tempRotation = startRotaion + angleIncrease * i;
            GameObject newProjectile = Instantiate(thisProjectile, playerPos, Quaternion.Euler(0f, 0f, tempRotation));

            GenericProjectile temp = newProjectile.GetComponent<GenericProjectile>();

            if (temp)
            {
                temp.SetUp(new Vector2(Mathf.Cos(tempRotation * Mathf.Deg2Rad), Mathf.Sin(tempRotation * Mathf.Deg2Rad)));
            }
        }
    }
}
