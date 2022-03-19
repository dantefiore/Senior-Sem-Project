using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : DungeonRoom
{
    public Door[] doors;
    public Vector2 newPlayerPos;
    public float enemyCount;
    public FloatValue death_count;
    [SerializeField] private GameObject reward;

    private void Start()
    {
        for(int i=0; i<enemies.Length; i++)
        {
            enemies[i].gameObject.SetActive(false);
        }

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
            reward.transform.position = new Vector3(0f, 60f, 0f);
            OpenDoors();
        }
        else
        {
            reward.transform.position = new Vector3(-30f, 60f, 0f);
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
        for (int i = 0; i < doors.Length; i++)
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
