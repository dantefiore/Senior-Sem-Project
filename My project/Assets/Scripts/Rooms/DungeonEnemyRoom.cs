using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEnemyRoom : DungeonRoom
{
    public Door[] doors;
    public Vector2 newPlayerPos;
    public float enemyCount;
    [SerializeField] public FloatValue death_count;

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
        if (death_count.RuntimeValue >= enemyCount)
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

            StartCoroutine(WaitCo());

            for (int i = 0; i < pots.Length; i++)
            {
                ChangeActivation(pots[i], true);
            }

            for(int i=0; i<enemies.Length; i++)
            {
                enemies[i].maxHealth.RuntimeValue = enemies[i].maxHealth.initialVal;
                enemies[i].health = enemies[i].maxHealth.initialVal;
            }

            death_count.RuntimeValue = 0;
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

    private IEnumerator WaitCo()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            yield return new WaitForSeconds(2);

            ChangeActivation(enemies[i], true);
            enemies[i].currState = EnemyState.idle;
            enemies[i].health = enemies[i].maxHealth.initialVal;
        }

    }
}
