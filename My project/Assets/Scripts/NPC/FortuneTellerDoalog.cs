using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FortuneTellerDoalog : Sign
{
    [SerializeField] private BoolVal goblinAttack;
    [SerializeField] private BoolVal dungeon1;
    [SerializeField] private BoolVal dungeon2;
    [SerializeField] private BoolVal dungeon3;
    [SerializeField] private BoolVal dungeon4;

    //[SerializeField] private BoolVal[] storyPoints;
    [SerializeField] private List<BoolVal> storyPoints = new List<BoolVal>();

    private string dialog1 = "Thank you for saving me! As a reward heres some information. " +
        "Theres a ruined temple in the woods, there are rumors of an special spell being held there. Go and get it.";
    private string dialog2 = "Go to the Desert Dungeon.";
    private string dialog3 = "Go to the Mountain Temple.";
    private string dialog4 = "Gp tp the old Castle.";

    public override void Update()
    {
        if (Input.GetButtonDown("Submit") && playerInRange)
        {
            if (dialogBox.activeInHierarchy)
            {
                dialogBox.SetActive(false);
            }
            else
            {
                dialogBox.SetActive(true);
                dialogText.text = dialog;
            }

            checkStory();
        }
    }

    private void checkStory()
    {
        int count = 0;

        for(int i =0; i < storyPoints.Count; i++)
        {
            if (storyPoints[i].RuntimeValue)
                count += 1;
        }

        if(count == 1)
        {
            dialogText.text = dialog1;
        }
        else if(count == 2)
        {
            dialogText.text = dialog2;
        }
        else if (count == 3)
        {
            dialogText.text = dialog3;
        }
        else if (count == 4)
        {
            dialogText.text = dialog4;
        }
    }
    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("npc") && !other.isTrigger)
        {
            context.Raise();
            playerInRange = true;
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("npc") && !other.isTrigger)
        {
            context.Raise();
            playerInRange = false;
        }
    }
}
