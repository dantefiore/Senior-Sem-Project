using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameSaveManager : MonoBehaviour
{
    public static GameSaveManager manager;  //the game save manager

    //the list of scriptable objects
    public List<ScriptableObject> objects = new List<ScriptableObject>();

    public FloatValue PlayerHealth; //the player's health
    public Inventory inv;   //the player's inventory
    public List<BoolVal> chests = new List<BoolVal>();  //the lists of bool vals

   // private void Awake()
    //{
 //       if (manager == null)
    //        manager = this;
    //    else
   //         Destroy(this.gameObject);

  //      DontDestroyOnLoad(this);
   // }

    //resets all scriptable objects
    public void ResetScriptables()
    {
        for(int i =0; i < objects.Count; i++)
        {
            if (File.Exists(Application.persistentDataPath + string.Format("/{0}.dat", i)))
            {
                File.Delete(Application.persistentDataPath + string.Format("/{0}.dat", i));
            }
        }
    }

    //closes all chests
    public void CloseChests()
    {
        for(int i =0; i < chests.Count; i++)
        {
            chests[i].RuntimeValue = false;
        }
    }

    //load all scriptables
    private void OnEnable()
    {
        LoadScriptables();
    }

    //save all scriptables
    private void OnDisable()
    {
        SaveScriptables();
    }

    public void SaveScriptables()
    {
        //creates a file and saves all scriptable objects
        for(int i =0; i < objects.Count; i++)
        {
            FileStream file = File.Create(Application.persistentDataPath + string.Format("/{0}.dat", i));
            BinaryFormatter newBinary = new BinaryFormatter();
            var json = JsonUtility.ToJson(objects[i]);
            newBinary.Serialize(file, json);
            file.Close();
        }
    }

    public void LoadScriptables()
    {
        //loads all scriptable objects
        for (int i = 0; i < objects.Count; i++)
        {
            if(File.Exists(Application.persistentDataPath + string.Format("/{0}.dat", i)))
            {
                FileStream file = File.Open(Application.persistentDataPath + string.Format("/{0}.dat", i), FileMode.Open);

                BinaryFormatter newBinary = new BinaryFormatter();
                JsonUtility.FromJsonOverwrite((string)newBinary.Deserialize(file), objects[i]);
                file.Close();
            }
        }
    }
}
