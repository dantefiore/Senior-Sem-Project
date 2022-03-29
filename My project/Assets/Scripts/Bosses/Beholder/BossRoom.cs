using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : DungeonRoom
{
    public Door[] doors;
    public Vector2 newPlayerPos;
    public float enemyCount;
    public FloatValue death_count;
    private Animator anim;
    [SerializeField] private GameObject reward;
    [SerializeField] private GameObject teleporter;

    [Header("For New Heart Pos")]
    [SerializeField] private float hc_x;
    [SerializeField] private float hc_y;

    [Header("Teleporter Pos")]
    [SerializeField] private float tp_x;
    [SerializeField] private float tp_y;

    [Header("Story Bools")]
    [SerializeField] private BoolVal storyBool;

    private void Start()
    {
        for(int i=0; i<enemies.Length; i++)
        {
            enemies[i].gameObject.SetActive(false);
        }

        anim = teleporter.GetComponent<Animator>();

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
            reward.transform.position = new Vector3(hc_x, hc_y, 0f);
            teleporter.transform.position = new Vector3(tp_x, tp_y, 0f);
            anim.SetBool("active", true);
            //Debug.Log("Enemies Defeated");
            storyBool.RuntimeValue = true;
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

            death_count.RuntimeValue = death_count.initialVal;
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
