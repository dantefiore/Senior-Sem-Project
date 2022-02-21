using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEnemyRoom : DungeonRoom
{
    public Door[] doors;
    public Vector2 newPlayerPos;
    public int enemyCount;

    private void Start()
    {
        enemyCount = enemies.Length;
        OpenDoors();
    }

    public void CheckEnemies()
    {
        if (GameObject.FindGameObjectsWithTag("DoorEnemy").Length <= 1)
        {
            OpenDoors();
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            other.transform.position = newPlayerPos;
            virtualCam.SetActive(true);

            for (int i = 0; i < enemies.Length; i++)
            {
                ChangeActivation(enemies[i], true);
                enemies[i].currState = EnemyState.idle;
                enemies[i].health = enemies[i].maxHealth.initialVal;
            }

            for (int i = 0; i < pots.Length; i++)
            {
                ChangeActivation(pots[i], true);
            }

            CloseDoors();
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
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
        for(int i = 0; i<doors.Length; i++)
        {
            doors[i].Close();
        }
    }

    public void OpenDoors()
    {
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].Open();
        }
    }
}
