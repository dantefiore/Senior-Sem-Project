using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public bool active = false; //the bool of the switches state
    public BoolVal storedVal;   //the stored bool of the switch
    public Sprite activeSprite; //the sprite of the switch
    private SpriteRenderer mySprite;    //the sprite of the switch
    public Door thisDoor;   //the door that the switch opens

    // Start is called before the first frame update
    void Start()
    {
        //the sprite depending on the state its in
        mySprite = GetComponent<SpriteRenderer>();
        active = storedVal.RuntimeValue;

        if (active)
        {
            ActivateSwitch();
        }
    }

    public void ActivateSwitch()
    {
        //sets the state to be active
        active = true;
        storedVal.RuntimeValue = active;
        thisDoor.Open();
        mySprite.sprite = activeSprite;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        //if the player collides with the button it calls the function
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            ActivateSwitch();
        }
    }
}
