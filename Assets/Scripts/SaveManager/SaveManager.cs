using NaughtyAttributes;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [Button]
    private void Save()
    {
        SaveSetup saveSetup = new SaveSetup();
        saveSetup.lastCheckpoint = 1;
        saveSetup.playerName = "Lucas";

        string setupToJASON = JsonUtility.ToJson(saveSetup, true);
        Debug.Log(setupToJASON);
        SaveFile(setupToJASON);
    }

    private void SaveFile(string json){
        string path = Application.persistentDataPath + "/save.txt";
        Debug.Log(path);
        //string fileLoaded = "";
        //if (File.Exists(path)) fileLoaded = File.ReadAllText(path);

        File.WriteAllText(path, json);
        
    }
}

[System.Serializable]
public class SaveSetup
{
    public int lastCheckpoint;
    public string playerName;
}
