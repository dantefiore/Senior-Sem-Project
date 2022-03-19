using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBeholder : TurretEnemy
{
    [SerializeField] private float speed;

    [Header("I Frames")]
    public Color flashColor; //the color the player flashes when hit
    public Color regColor;  //the characters normal colors
    public float flashDur; //how long the flashing lasts
    public int numOfFlash;  //the number of flashes
    public Collider2D triggerCollider; //the players hurt box
    public SpriteRenderer mySprite; //the characters sprite

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 vectorToTarget = target.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
    }

    public override IEnumerator KnockCo(Rigidbody2D myRigidBody, float knockTime)
    {
        if (myRigidBody != null)
        {
            StartCoroutine(FlashCo());
        }

        return base.KnockCo(myRigidBody, knockTime);
    }

    private IEnumerator FlashCo()
    {
        //plays the flashing animations
        for (int i = 0; i < numOfFlash; i++)
        {
            mySprite.color = flashColor;
            yield return new WaitForSeconds(flashDur);
            mySprite.color = regColor;
            yield return new WaitForSeconds(flashDur);
        }

        //mySprite.color = regColor;
    }
}
