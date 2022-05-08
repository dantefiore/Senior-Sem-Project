using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : DungeonRoom //extends dungeon room
{
    public Door[] doors;    //holds the amount of doors there are
    public Vector2 newPlayerPos; //sets the new player pos
    public float enemyCount;    //how many things need to be defeated before the doors open and rewards spawn
    public FloatValue death_count;  //counts when the boss was killed
    private Animator anim; //the animator
    [SerializeField] private GameObject reward; //the reward for killing the boss
    [SerializeField] private GameObject teleporter; //the teleporter to leave the dungeon

    [Header("For New Heart Pos")]
    [SerializeField] private float hc_x;    //x location for the reward
    [SerializeField] private float hc_y;    //y location for the reward

    [Header("Teleporter Pos")]
    [SerializeField] private float tp_x;    //x location for the teleporter
    [SerializeField] private float tp_y;    //y location for the teleporter

    [Header("Story Bools")]
    [SerializeField] private BoolVal storyBool; //what bool will cahnge to tru ewhen the boss is defeated

    private void Start()
    {
        //when the scene starts the enemies dont appear
        for(int i=0; i<enemies.Length; i++)
        {
            enemies[i].gameObject.SetActive(false);
        }

        //sets the animator for the teleporter
        anim = teleporter.GetComponent<Animator>();

        OpenDoors();    //calls the function
    }

    private void Update()
    {
        CheckEnemies(); //checks if the player defeated all enemies
    }

    private void CheckEnemies()
    {
        //if the player defeated all the enemies
        if (death_count.RuntimeValue >= enemyCount)
        {
            if(reward != null)  //spawn the reward
                reward.transform.position = new Vector3(hc_x, hc_y, 0f);

            //spawn the teleporter
            teleporter.transform.position = new Vector3(tp_x, tp_y, 0f);
            anim.SetBool("active", true);   //animate the teleporter
            storyBool.RuntimeValue = true;  //change the story bool
            OpenDoors();    //call the open doors function
        }
    }
    
    //when the player enters the trigger area
    public override void OnTriggerEnter2D(Collider2D other)
    {
        //if it is the player that enters it
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            //change the players position and change the camera
            other.transform.position = newPlayerPos;
            virtualCam.SetActive(true);

            StartCoroutine(WaitCo());   //calls the wait Coroutine

            //if there are pots in the room they appear
            for (int i = 0; i < pots.Length; i++)
            {
                ChangeActivation(pots[i], true);
            }

            //set teh enemy counter to 0 is it wasn't already
            death_count.RuntimeValue = death_count.initialVal;
            CloseDoors();   //calls the close doors function
        }
    }

    //if the player exits the trigger area
    public override void OnTriggerExit2D(Collider2D other)
    {
        //checks if its the player
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            virtualCam.SetActive(true); //change camera

            //enemies disappear
            for (int i = 0; i < enemies.Length; i++)
            {
                ChangeActivation(enemies[i], false);
            }

            //pots disappear
            for (int i = 0; i < pots.Length; i++)
            {
                ChangeActivation(pots[i], false);
            }
        }
    }

    public void CloseDoors()
    {
        //closes all doors in the array
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].Close();
        }
    }

    public void OpenDoors()
    {
        //opens all doors in the array
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].Open();
        }
    }

    private IEnumerator WaitCo()
    {
        //forf each enemy in the array
        for (int i = 0; i < enemies.Length; i++)
        {
            //wait for 2 seconds
            yield return new WaitForSeconds(2);

            //spawn the first enemy in the array
            ChangeActivation(enemies[i], true);
            enemies[i].currState = EnemyState.idle;
            enemies[i].health = enemies[i].maxHealth.initialVal;
        }

    }
}
