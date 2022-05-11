using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NagaHead : Naga
{
    [Header("I Frames")]
    public Color flashColor; //the color the player flashes when hit
    public Color regColor;  //the characters normal colors
    public float flashDur; //how long the flashing lasts
    public int numOfFlash;  //the number of flashes
    public Collider2D triggerCollider; //the players hurt box
    public SpriteRenderer mySprite; //the characters sprite

    //if the boss has been hit
    public override IEnumerator KnockCo(Rigidbody2D myRigidBody, float knockTime)
    {
        if (myRigidBody != null)
        {   //flash the colors
            StartCoroutine(FlashCo());
        }

        return base.KnockCo(myRigidBody, knockTime);
    }

    private IEnumerator FlashCo()
    {
        //plays the flashing animations
        for (int i = 0; i < numOfFlash; i++)
        {
            //repeatedly change the colors
            mySprite.color = flashColor;
            yield return new WaitForSeconds(flashDur);
            mySprite.color = regColor;
            yield return new WaitForSeconds(flashDur);
        }

        //mySprite.color = regColor;
    }
}
