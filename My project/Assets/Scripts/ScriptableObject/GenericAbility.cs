using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObject/Ability/Generic Ability", fileName = "New Generic Ability")]
public class GenericAbility : ScriptableObject
{
    public float magicCost; //the magic cost to use the ability
    public float duration;  //how long the ability lasts

    public Inventory playerMagic;   //the player's inventory
    //public Float magic;
    //public SignalSender useMagic;

    public virtual void Ability(Vector2 playerPos, Vector2 playerDir, Animator anim = null, Rigidbody2D myRigidBody = null)
    {

    }
}
