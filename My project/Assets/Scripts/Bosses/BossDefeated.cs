using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossDefeated : MonoBehaviour
{
    [SerializeField] private FloatValue finalBoss; //counts if the final boss was killed

    // Update is called once per frame
    void Update()
    {
        if (finalBoss.RuntimeValue > 0) //if the total is higher than 0
            SceneManager.LoadScene("Win Screen"); //send to win screen
    }
}