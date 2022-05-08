using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class TitleScreen : MonoBehaviour
{
    //liost of scriptable objects
    public List<ScriptableObject> objects = new List<ScriptableObject>();
    public List<BoolVal> chests = new List<BoolVal>();  //lost of ool vals
    public FloatValue playerHealth; //player's health
    public PlayerMovement playerMovement;   //the plsyer movement script
    public Inventory inv;   //the player's inventory
    public FloatValue heartContainers;  //the health ui

    //resets everything for the new game
    public void newGame()
    {
        inv.currMagic = inv.maxMagic;
        playerHealth.RuntimeValue = 6f;
        inv.items.Clear();
        inv.numKeys = 0;
        inv.coins = 0;
        heartContainers.RuntimeValue = 3;

        for(int i =0; i < chests.Count; i++)
        {
            chests[i].RuntimeValue = false;
        }

        for (int i = 0; i < objects.Count; i++)
        {
            if (File.Exists(Application.persistentDataPath + string.Format("/{0}.dat", i)))
            {
                File.Delete(Application.persistentDataPath + string.Format("/{0}.dat", i));
            }
        }

        SceneManager.LoadScene("Opening Cutscene");
    }
    
    public void loadGame()
    {   //loads the saved game file
        SceneManager.LoadScene(PlayerPrefs.GetInt("SavedScene"));
    }

    public void toTitle()
    {
        //sends to the title screen
        SceneManager.LoadScene("Title Screen");
    }

    public void howToPlay()
    {
        //sends to the how to play screen
        SceneManager.LoadScene("How To Play Screen");
    }

    public void credits()
    {
        //sends to the credits screen
        SceneManager.LoadScene("Credits");
    }

    public void QuitToDesktop()
    {
        //closes the game
        Application.Quit();
    }
}
