using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public FloatValue currHealth;
    public SignalSender playerHealthSignal;
    public VectorValue startPos;
    public Inventory playerInv;
    public SpriteRenderer recievedItemSprite;
    public SignalSender playerHit;

    [Header("Magic")]
    public GameObject projectile;
    public SignalSender reduceMagic;
    public MagicManager magic;
    public Item waterMagic;

    [Header("I Frames")]
    public Color flashColor;
    public Color regColor;
    public float flashDur;
    public int numOfFlash;
    public Collider2D triggerCollider;
    public SpriteRenderer mySprite;

    // Start is called before the first frame update
    void Start()
    {
        currentState = PlayerState.walk;
        anim = GetComponent<Animator>(); //finish setting up the animator

        anim.SetFloat("moveX", 0);
        anim.SetFloat("moveY", -1);

        myRigidBody = GetComponent<Rigidbody2D>(); //finish setting up the rigid body

        transform.position = startPos.initialVal;
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

        if (Input.GetButtonDown("attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger)
        {
            StartCoroutine(AttackCo());
        }
        else if (Input.GetButtonDown("second attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger)
        {
            if(playerInv.checkForItem(waterMagic))
                StartCoroutine(SecondAttackCo());
        }
        else if(currentState == PlayerState.walk || currentState == PlayerState.idle)
        {
            UpdateAnimAndMove();
        }
    }

    private IEnumerator AttackCo()
    {
        anim.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return null; //waits 1 frame
        anim.SetBool("attacking", false);
        yield return new WaitForSeconds(0.3f);

        if(currentState != PlayerState.interact)
        {
            currentState = PlayerState.walk;
        }
    }

    private IEnumerator SecondAttackCo()
    {
        anim.SetBool("attack2", true);
        currentState = PlayerState.attack;
        yield return null; //waits 1 frame
        makeWater();
        anim.SetBool("attack2", false);
        yield return new WaitForSeconds(0.3f);

        if (currentState != PlayerState.interact)
        {
            currentState = PlayerState.walk;
        }
    }

    private void makeWater()
    {
        if(playerInv.currMagic > 0)
        {
            Vector2 temp = new Vector2(anim.GetFloat("moveX"), anim.GetFloat("moveY"));
            Arrow arrow = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Arrow>();
            arrow.SetUp(temp, ChooseArrowDirection());
            magic.DecreaseMagic(1);
        }
       
    }

    Vector3 ChooseArrowDirection()
    {
        float temp = Mathf.Atan2(anim.GetFloat("moveY"), anim.GetFloat("moveX")) * Mathf.Rad2Deg;
        return new Vector3(0, 0, temp);
    }

    public void RaiseItem()
    {
        if (playerInv.currItem != null) 
        { 
            if(currentState != PlayerState.interact)
            {
                anim.SetBool("Item Recieved", true);
                currentState = PlayerState.interact;
                recievedItemSprite.sprite = playerInv.currItem.itemSprite;
            }
            else
            {
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

    public void Knock(float knockTime, float dmg)
    {
        currHealth.RuntimeValue -= dmg;
        playerHealthSignal.Raise();
        
        if(currHealth.RuntimeValue > 0)
        {
            StartCoroutine(KnockCo(knockTime));
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

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

    private IEnumerator FlashCo()
    {
        triggerCollider.enabled = false;
        for (int i = 0; i < numOfFlash; i++){
            mySprite.color = flashColor;
            yield return new WaitForSeconds(flashDur);
            mySprite.color = regColor;
            yield return new WaitForSeconds(flashDur);
        }
        triggerCollider.enabled = true;
    }
}