using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC1 : Sign
{
    //a bool val that impacts the npc dialog
    [SerializeField] private BoolVal goblinAttack;
    
    //the texts of the npc
    [SerializeField] private string text1 = "";
    [SerializeField] private string text2 = "";

    //lists of story points
    [SerializeField] private List<BoolVal> storyPoints = new List<BoolVal>();

    public override void Update()
    {
        //sees if the player is in range of the npc
        //and it they pressed the button to talk
        if (Input.GetButtonDown("Submit") && playerInRange)
        {
            //if the dialog box is open, it then closes
            if (dialogBox.activeInHierarchy)
            {
                dialogBox.SetActive(false);
            }
            else  //if the dialog box is closed, it then opens
            {
                dialogBox.SetActive(true);
                dialogText.text = dialog;
            }

            checkStory();
        }
    }

    private void checkStory()
    {
        //checks if which text should appear
        if (goblinAttack.RuntimeValue)
            dialogText.text = text2;
        else if (!goblinAttack.RuntimeValue)
            dialogText.text = text1;
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        //if the player is in range to talk
        if (other.CompareTag("npc") && !other.isTrigger)
        {
            context.Raise();
            playerInRange = true;
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        //if the player is not in range to talk
        if (other.CompareTag("npc") && !other.isTrigger)
        {
            context.Raise();
            playerInRange = false;
        }
    }
}
