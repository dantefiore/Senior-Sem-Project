using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : Interactable
{ 
    public Item contents; //contents of the chest
    public Inventory inv;   //the player's inventory
    public bool isOpened;   //if the chest is opened
    public BoolVal storeState;  //if the chest was opened already
    public SignalSender raiseItem;  //the signal sender for the item
    public GameObject dialogBox;    //the player's dialog box
    public Text dialogText; //the text of the dialog box
    private Animator anim;  //the animator of the chest
    public PlayerInventory playerInv;   //the player's inventory
    public InventoryItem thisItem;  //the item in the player's inventory

    // Start is called before the first frame update
    void Start()
    {
        //the chest is opened or closed
        //depending on the store state
        anim = GetComponent<Animator>();
        isOpened = storeState.RuntimeValue;
        if (isOpened)
        {
            anim.SetBool("opened", true);
        }
        else
            anim.SetBool("opened", false);
    }

    // Update is called once per frame
    void Update()
    {
        //if the player is in range to open the chest
        if (Input.GetButtonDown("Submit") && playerInRange)
        {
            if (!isOpened)
            {
                //chest is not opened
                OpenChest();
            }
            else
            {
                //chest is already opened
                ChestWasOpened();
            }
        }
    }

    public void OpenChest()
    {
        //dialog box on
        dialogBox.SetActive(true);

        //dialog text = contents text
        dialogText.text = contents.itemName;

        //add contents to the inventory
        inv.AddItem(contents);
        inv.currItem = contents;

        //adds the item to the player's inventory
        if (playerInv && thisItem)
        {
            if (playerInv.myInv.Contains(thisItem))
            {
                thisItem.numHeld += 1;
            }
            else
            {
                playerInv.myInv.Add(thisItem);
                thisItem.numHeld += 1;
            }
        }

        //raise the signal to the player to animate
        raiseItem.Raise();
        
        //raise the context clue
        context.Raise();

        //set the chest to opened
        isOpened = true;
        anim.SetBool("opened", true);

        storeState.RuntimeValue = isOpened;
    }

    public void ChestWasOpened()
    {
        //dialog off
        dialogBox.SetActive(false);

        //raise the signal to the player to stop animating
        raiseItem.Raise();
        playerInRange = false;
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        //raises the bubble aboves the players head if they are in range to open
        if (other.CompareTag("Player") && !other.isTrigger && !isOpened)
        {
            context.Raise();
            playerInRange = true;
        }
    }
    public override void OnTriggerExit2D(Collider2D other)
    {
        //closes the bubble aboves the players head if they are not in range to open
        if (other.CompareTag("Player") && !other.isTrigger && !isOpened)
        {
            context.Raise();
            playerInRange = false;
        }
    }
}
