using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : Interactable
{
    public Item contents;
    public Inventory inv;
    public bool isOpened;
    public BoolVal storeState;
    public SignalSender raiseItem;
    public GameObject dialogBox;
    public Text dialogText;
    private Animator anim;
    public PlayerInventory playerInv;
    public InventoryItem thisItem;

    // Start is called before the first frame update
    void Start()
    {
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
        if (other.CompareTag("Player") && !other.isTrigger && !isOpened)
        {
            context.Raise();
            playerInRange = true;
        }
    }
    public override void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !isOpened)
        {
            context.Raise();
            playerInRange = false;
        }
    }
}
