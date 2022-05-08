using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class InventorySaver : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInv;

    private void OnEnable()
    {
       // playerInv.myInv.Clear();
        LoadScriptables();
    }

    private void OnDisable()
    {
        SaveScriptables();  //saves all the inventory information
    }

    public void ResetScriptables()
    {   //resets the inventory info
        int i = 0;
        while (File.Exists(Application.persistentDataPath + string.Format("/{0}.inv", i)))
        {
            File.Delete(Application.persistentDataPath + string.Format("/{0}.inv", i));
            i++;
        }
    }

    public void SaveScriptables()
    {
        ResetScriptables();

        //formats and saves the inventory information
        for (int i = 0; i < playerInv.myInv.Count; i++)
        {
            FileStream file = File.Create(Application.persistentDataPath + string.Format("/{0}.inv", i));
            BinaryFormatter newBinary = new BinaryFormatter();
            var json = JsonUtility.ToJson(playerInv.myInv[i]);
            newBinary.Serialize(file, json);
            file.Close();
        }
    }

    public void LoadScriptables()
    {
        ResetScriptables();
        int i = 0;

        //loads the file that holds the inventory information
        while(File.Exists(Application.persistentDataPath + string.Format("/{0}.inv", i)))
        {
            var temp = ScriptableObject.CreateInstance<InventoryItem>();

            FileStream file = File.Open(Application.persistentDataPath + string.Format("/{0}.dat", i), FileMode.Open);

            BinaryFormatter newBinary = new BinaryFormatter();
            JsonUtility.FromJsonOverwrite((string)newBinary.Deserialize(file), temp);
            file.Close();
            playerInv.myInv.Add(temp);

            i++;
        }
    }
}

