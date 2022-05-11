using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FortuneTellerDoalog : Sign
{
    //the different story points that changes the dialog
    [SerializeField] private BoolVal goblinAttack;
    [SerializeField] private BoolVal dungeon1;
    [SerializeField] private BoolVal dungeon2;
    
    //the array of the story points
    [SerializeField] private List<BoolVal> storyPoints = new List<BoolVal>();

    //the dialog that happens when the first story point is done
    private string dialog1 = "Thank you for saving me but there is more monsters to come! " +
                                "There is a cave in the mountains where a devilish one lives.";

    //the dialog after the first dungeon is completed
    private string dialog2 = "Good job but there is one more, a freakish snake lives in the underground caverns. " +
                                "Seek it out and the village might be saved";

    public override void Update()
    {
        //when the player presses the button to talk to the npc
        if (Input.GetButtonDown("Submit") && playerInRange)
        {
            //if the dialog box is open, it then closes
            if (dialogBox.activeInHierarchy)
            {
                dialogBox.SetActive(false);
            }
            else  //if the dialog box is closed, it opens
            {
                dialogBox.SetActive(true);
                dialogText.text = dialog;
            }

            checkStory();  //calls this function
        }
    }

    private void checkStory()
    {
        int count = 0;  
        
        //will determine how many story points happened
        for(int i =0; i < storyPoints.Count; i++)
        {
            if (storyPoints[i].RuntimeValue)
                count += 1;
        }

        //will change the dialog depending on how
        //many story points were completed
        if(count == 1)
        {
            dialogText.text = dialog1;
        }
        else if(count == 2)
        {
            dialogText.text = dialog2;
        }
    }
    public override void OnTriggerEnter2D(Collider2D other)
    {
        //when the player enters the talk range of the npc,
        //it allows the person to talk to them
        if (other.CompareTag("npc") && !other.isTrigger)
        {
            context.Raise();
            playerInRange = true;
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        //when the player leaves the talking range,
        //it stops letting the player talk to them
        if (other.CompareTag("npc") && !other.isTrigger)
        {
            context.Raise();
            playerInRange = false;
        }
    }
}
