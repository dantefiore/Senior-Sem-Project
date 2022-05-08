using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC1 : Sign
{
    [SerializeField] private BoolVal goblinAttack;
    [SerializeField] private string text1 = "";
    [SerializeField] private string text2 = "";

    //[SerializeField] private BoolVal[] storyPoints;
    [SerializeField] private List<BoolVal> storyPoints = new List<BoolVal>();

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
        if (goblinAttack.RuntimeValue)
            dialogText.text = text2;
        else if (!goblinAttack.RuntimeValue)
            dialogText.text = text1;
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
