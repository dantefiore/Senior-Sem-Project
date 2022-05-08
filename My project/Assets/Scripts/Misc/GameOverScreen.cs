using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameOverScreen : TitleScreen
{
    [SerializeField] private FloatValue health; //the player health
    [SerializeField] private FloatValue containers; //the health ui amount
    private int scene;  //the scene the player is in
    [SerializeField] private FloatValue sceneToCont;    //the scene that needs to be reloaded
    public List<BoolVal> boolsToReset = new List<BoolVal>();    //the amount of bool vals that needs to be reset

    //what happens when the retry button is clicked
    public void retry()
    {
        //loads the last scene the player was in and resets the health to max
        scene = (int)sceneToCont.RuntimeValue;
        health.RuntimeValue = containers.RuntimeValue * 2;
        scene = PlayerPrefs.GetInt("DeathScene");

        //aslong as the scene isn't the tile, how to play, game over, or credits screen
        //the last scene the player was in is reloaded
        if (sceneToCont.RuntimeValue != 0 || sceneToCont.RuntimeValue != 1 
            || sceneToCont.RuntimeValue != 8 || sceneToCont.RuntimeValue != 13)
        {
            SceneManager.LoadScene((int)sceneToCont.RuntimeValue);
            ResetBools();
        }
        else
            return;
    }

    public void ResetBools()
    {
        //resets all the bools in the list
        for (int i = 0; i < chests.Count; i++)
        {
            chests[i].RuntimeValue = false;
        }
    }
}
