using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameSaveManager : MonoBehaviour
{
    public static GameSaveManager manager;
    public List<ScriptableObject> objects = new List<ScriptableObject>();

    public FloatValue PlayerHealth;
    public Inventory inv;
    public List<BoolVal> chests = new List<BoolVal>();

   // private void Awake()
    //{
 //       if (manager == null)
    //        manager = this;
    //    else
   //         Destroy(this.gameObject);

  //      DontDestroyOnLoad(this);
   // }

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

    public void ClearSpells()
    {
        inv.items.Clear();
    }

    public void FillMagic()
    {
        inv.currMagic = inv.maxMagic;
    }

    public void FullHeal()
    {
        PlayerHealth.RuntimeValue = PlayerHealth.initialVal;
    }

    public void AddKeys()
    {
        inv.numKeys += 10;
    }

    public void CloseChests()
    {
        for(int i =0; i < chests.Count; i++)
        {
            chests[i].RuntimeValue = false;
        }
    }

    private void OnEnable()
    {
        LoadScriptables();
    }

    private void OnDisable()
    {
        SaveScriptables();
    }

    public void SaveScriptables()
    {
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
