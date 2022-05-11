using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : Interactable
{
    public GameObject dialogBox;    //the dialog box
    public Text dialogText; //the dialog text
    public string dialog;   //the dialog that is said

    // Update is called once per frame
    public virtual void Update()
    {
        if (Input.GetButtonDown("Submit") && playerInRange)
        {
            //if the dialog box is opened, its then is closed
            if (dialogBox.activeInHierarchy)
            {
                dialogBox.SetActive(false);
            }
            else  //if the dialog box is closed, its then opened
            {
                dialogBox.SetActive(true);
                dialogText.text = dialog;
            }
        }
    }
    public override void OnTriggerExit2D(Collider2D other)
    {
        //if the player leaves the range, the dialog box is closed
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            context.Raise();

            playerInRange = false;
            dialogBox.SetActive(false);
        }
    }

}
