using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEnemyRoom : DungeonRoom
{
    public Door[] doors;    //the doors on the room
    public Vector2 newPlayerPos;    //the player position
    public float enemyCount;    //the amount of enemies needed to open the doors
    [SerializeField] public FloatValue death_count; //the amount of enemies defeated

    private void Start()
    {
        //enemyCount = enemies.Length;
        OpenDoors();
    }

    private void Update()
    {
            CheckEnemies();
    }

    private void CheckEnemies()
    {
        //checks if the death counter is at or higher than the required amount
        if (death_count.RuntimeValue >= enemyCount)
        {
            //opens the doors
            OpenDoors();
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        //if the player enters the trigger area
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            //move the player inside the room
            other.transform.position = newPlayerPos;
            virtualCam.SetActive(true);

            //waits to spawn each enemy
            StartCoroutine(WaitCo());

            //spawns each of the pots
            for (int i = 0; i < pots.Length; i++)
            {
                ChangeActivation(pots[i], true);
            }

            //spawns each of the enemies
            for(int i=0; i<enemies.Length; i++)
            {
                enemies[i].maxHealth.RuntimeValue = enemies[i].maxHealth.initialVal;
                enemies[i].health = enemies[i].maxHealth.initialVal;
            }

            //resets the counter to 0, and closes the door
            death_count.RuntimeValue = 0;
            CloseDoors();
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        //when the player exits the room, all the enemies and pots disappear
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            virtualCam.SetActive(true);

            for (int i = 0; i < enemies.Length; i++)
            {
                ChangeActivation(enemies[i], false);
            }

            for (int i = 0; i < pots.Length; i++)
            {
                ChangeActivation(pots[i], false);
            }
        }
    }

    public void CloseDoors()
    {
        //closes all doors in the room
        for(int i = 0; i<doors.Length; i++)
        {
            doors[i].Close();
        }
    }

    public void OpenDoors()
    {
        //opens all the doors in the room
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].Open();
        }
    }

    private IEnumerator WaitCo()
    {
        //waits for a ceratain amount of time,
        //then sets all the enemies and pots health
        for (int i = 0; i < enemies.Length; i++)
        {
            yield return new WaitForSeconds(2);

            ChangeActivation(enemies[i], true);
            enemies[i].currState = EnemyState.idle;
            enemies[i].health = enemies[i].maxHealth.initialVal;
        }

    }
}
