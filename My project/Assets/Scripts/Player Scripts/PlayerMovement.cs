using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//the states the player can be in
 public enum PlayerState { idle, walk, attack, interact, stagger }

public class PlayerMovement : MonoBehaviour
{
    [Header("Character Movement")]
    public float speed; //the speed of the player
    private Rigidbody2D myRigidBody; //the rigid body object on the player
    private Vector3 change; //the change in position for the player
    private Animator anim; //controls what animations play
    public PlayerState currentState;

    [Header("Character Other")]
    /*public FloatValue currHealth; //the current health of the player
    public SignalSender playerHealthSignal; //the signal that is recieved when the health should be changed*/

    public FloatValue heartContainers; //the amount of heart containers the player has
    public VectorValue startPos;    //stored start position
    public Inventory playerInv; //the inventory of the player
    public SpriteRenderer recievedItemSprite;   //the sprite that the player holds when they open a chest
    public SignalSender playerHit; //when the player is hit by an enemy

    [Header("Magic")]
    public GameObject projectile;   //the water magic sprite
    public SignalSender reduceMagic;    //a singal that is sent when the player uses an ability that reduces magic
    public MagicManager magic;  //the amount of the the player has
    public Item waterMagic; //the properties of the water arrow ability

    [Header("I Frames")]
    public Color flashColor; //the color the player flashes when hit
    public Color regColor;  //the characters normal colors
    public float flashDur; //how long the flashing lasts
    public int numOfFlash;  //the number of flashes
    public Collider2D triggerCollider; //the players hurt box
    public SpriteRenderer mySprite; //the characters sprite

    // Start is called before the first frame update
    void Start()
    {
        currentState = PlayerState.walk; //sets the player state
        anim = GetComponent<Animator>(); //finish setting up the animator

        //for the animations and which way the character should be facing
        anim.SetFloat("moveX", 0);
        anim.SetFloat("moveY", -1);

        myRigidBody = GetComponent<Rigidbody2D>(); //finish setting up the rigid body

        transform.position = startPos.initialVal;   //the starting position of the character
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == PlayerState.interact)
        {
            return;
        }

        change = Vector3.zero; //sets the change variable to 0
        change.x = Input.GetAxisRaw("Horizontal"); //gets the input fo rthe x direction
        change.y = Input.GetAxisRaw("Vertical"); //gets the input for the y direction

        //checks what the player is pressing an attack button and if they are able to attack
        if (Input.GetButtonDown("attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger)
        {
            StartCoroutine(AttackCo()); //starts the attack function
        }
        else if (Input.GetButtonDown("second attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger)
        {
            if(playerInv.checkForItem(waterMagic)) //if the player has this ability
                StartCoroutine(SecondAttackCo());   //starts the second attack function
        }
        else if(currentState == PlayerState.walk || currentState == PlayerState.idle)
        {
            UpdateAnimAndMove(); //changes the characters animations
        }

        /*//checks the health to see if it went over the max health
        if (currHealth.RuntimeValue > heartContainers.RuntimeValue * 2f)
        {
            currHealth.RuntimeValue = heartContainers.RuntimeValue * 2f;
        }*/
    }

    //plays the attack animation
    private IEnumerator AttackCo()
    {
        anim.SetBool("attacking", true);    //to play the attack animation
        currentState = PlayerState.attack;
        yield return null; //waits 1 frame
        anim.SetBool("attacking", false);
        yield return new WaitForSeconds(0.3f);  //runs for 1/3 of a second

        //if the button is pressed while in the interact state
        if(currentState != PlayerState.interact)
        {
            //changes the player to the walk state
            currentState = PlayerState.walk;
        }
    }

    //plays the animation for the abilities
    private IEnumerator SecondAttackCo()
    {
        anim.SetBool("attack2", true);
        currentState = PlayerState.attack;
        yield return null; //waits 1 frame
        makeWater();    //runs the make water function
        anim.SetBool("attack2", false);
        yield return new WaitForSeconds(0.3f);

        //if the button is pressed while in the interact state
        if (currentState != PlayerState.interact)
        {
            //changes the player to the walk state
            currentState = PlayerState.walk;
        }
    }

    private void makeWater()
    {
        //makes sure the player unlocked it
        if(playerInv.currMagic > 0)
        {
            //gets the arrow should be faced
            Vector2 temp = new Vector2(anim.GetFloat("moveX"), anim.GetFloat("moveY"));
            Arrow arrow = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Arrow>();
            arrow.SetUp(temp, ChooseArrowDirection());
            magic.DecreaseMagic(1); //decrease the amount of magic the player has
        }
       
    }

    Vector3 ChooseArrowDirection()
    {
        //gets the direction of the arrow should face
        float temp = Mathf.Atan2(anim.GetFloat("moveY"), anim.GetFloat("moveX")) * Mathf.Rad2Deg;
        return new Vector3(0, 0, temp);
    }

    public void RaiseItem()
    {
        if (playerInv.currItem != null) 
        { 
            //if the player is not in the inmteract state already
            if(currentState != PlayerState.interact)
            {
                //plays the recieved animation
                currentState = PlayerState.interact;
                anim.SetBool("Item Recieved", true);
                recievedItemSprite.sprite = playerInv.currItem.itemSprite;
            }
            else
            {
                //stops the recieved animation
                anim.SetBool("Item Recieved", false);
                currentState = PlayerState.idle;
                recievedItemSprite.sprite = null;
                playerInv.currItem = null;
            }
        }
    }

    //Changes the animations for what should be played when
    void UpdateAnimAndMove()
    {
        if (change != Vector3.zero) //if change is anything but 0
        {
            MoveCharacter();

            change.x = Mathf.Round(change.x);
            change.y = Mathf.Round(change.y);

            //plays the animation depending on what number is entered
            anim.SetFloat("moveX", change.x); 
            anim.SetFloat("moveY", change.y);
            anim.SetBool("moving", true);
        }
        else
        {
            anim.SetBool("moving", false); //idle positions
        }
    }

    //changes the position of the character
    void MoveCharacter()
    {
        change.Normalize();
        myRigidBody.MovePosition(
            transform.position + change * speed * Time.deltaTime
         );
    }

    //how much damage and how long the player is in knock for
    public void Knock(float knockTime)
    {
        StartCoroutine(KnockCo(knockTime));

        /*
        //lowers the health
        currHealth.RuntimeValue -= dmg;
        playerHealthSignal.Raise();
        
        //if the player lived or died
        if(currHealth.RuntimeValue > 0)
        {
            StartCoroutine(KnockCo(knockTime));
        }
        else
        {
            this.gameObject.SetActive(false);
        }*/
    }

    //moving the character back
    private IEnumerator KnockCo(float knockTime)
    {
        playerHit.Raise();

        if (myRigidBody != null)
        {
            StartCoroutine(FlashCo());
            yield return new WaitForSeconds(knockTime);
            myRigidBody.velocity = Vector2.zero;

            currentState = PlayerState.idle;
            myRigidBody.velocity = Vector2.zero;
        }
    }

    //makes the Invulnerability frames
    private IEnumerator FlashCo()
    {
        //turns off the players hurtbox
        triggerCollider.enabled = false;

        //plays the flashing animations
        for (int i = 0; i < numOfFlash; i++){
            mySprite.color = flashColor;
            yield return new WaitForSeconds(flashDur);
            mySprite.color = regColor;
            yield return new WaitForSeconds(flashDur);
        }

        //turns the hurtbox back on
        triggerCollider.enabled = true;
    }
}