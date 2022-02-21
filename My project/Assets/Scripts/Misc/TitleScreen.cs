using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class TitleScreen : MonoBehaviour
{
    public List<ScriptableObject> objects = new List<ScriptableObject>();
    public List<BoolVal> chests = new List<BoolVal>();
    public FloatValue playerHealth;
    public PlayerMovement playerMovement;
    public Inventory inv;
    public FloatValue heartContainers;

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
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("SavedScene"));
    }

    public void toTitle()
    {
        SceneManager.LoadScene("Title Screen");
    }

    public void howToPlay()
    {
        SceneManager.LoadScene("How To Play Screen");
    }

    public void QuitToDesktop()
    {
        Application.Quit();
    }
}
