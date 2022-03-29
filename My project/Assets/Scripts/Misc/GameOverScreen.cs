using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameOverScreen : TitleScreen
{
    [SerializeField] private FloatValue health;
    [SerializeField] private FloatValue containers;
    private int scene;
    [SerializeField] private FloatValue sceneToCont;


    public void retry()
    {
        scene = (int)sceneToCont.RuntimeValue;
        health.RuntimeValue = containers.RuntimeValue * 2;
        scene = PlayerPrefs.GetInt("DeathScene");

        if (sceneToCont.RuntimeValue != 0 || sceneToCont.RuntimeValue != 1 || sceneToCont.RuntimeValue != 7)
        {
            SceneManager.LoadScene((int)sceneToCont.RuntimeValue);
        }
        else
            return;
    }
}
