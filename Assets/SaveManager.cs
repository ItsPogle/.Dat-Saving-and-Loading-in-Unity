using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class SaveData
{
    public int experience = 0;
    public List<int> inventory = new List<int>(3);
}

public class SaveManager : MonoBehaviour
{
    public static SaveManager Singleton;

    public SaveData playerData { get; set; } = new SaveData();

    [SerializeField] Player myPlayer;

    void Awake() 
    {
        SaveManager.Singleton = this;
        Load();
        myPlayer.LoadData();
    }

    void OnApplicationQuit()
    {
        myPlayer.SaveInventory();
        Save(); 
    }

    void Save()
    {
        FileStream file = new FileStream(Application.persistentDataPath + "/player.dat", FileMode.OpenOrCreate);

        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(file, playerData);
        }
        catch (SerializationException e)
        {
            Debug.LogError(e.Message);
        }
        finally
        {
            file.Close();
        }
    }

    void Load()
    {
        if(File.Exists(Application.persistentDataPath + "/player.dat"))
        {
            FileStream file = new FileStream(Application.persistentDataPath + "/player.dat", FileMode.Open);

            if(file.Length > 0)
            {
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    playerData = formatter.Deserialize(file) as SaveData;
                }
                catch (SerializationException e)
                {
                    Debug.LogError(e.Message);
                }
                finally
                {
                    file.Close();
                }
            }
            else
            {
                Debug.Log("Deleted empty file...");
                file.Dispose();
                File.Delete(Application.persistentDataPath + "/player.dat");
            }
        }
    }
}
