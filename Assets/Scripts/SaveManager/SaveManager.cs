using NaughtyAttributes;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;

public class SaveManager : Singleton<SaveManager>
{
    private SaveSetup _saveSetup;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this.gameObject);
        _saveSetup = new SaveSetup();
        _saveSetup.lastLevel = 2;
        _saveSetup.playerName = "Lucas";
    }
    

    #region SAVE
    [Button]
    private void Save()
    {
        string setupToJASON = JsonUtility.ToJson(_saveSetup, true);
        Debug.Log(setupToJASON);
        SaveFile(setupToJASON);
    }

    public void SaveName(string name)
    {
        _saveSetup.playerName = name;
        Save();
    }

    public void SaveLastLevel(int level)
    {
        _saveSetup.lastLevel = level;
        SaveItems();
        Save();
    }

    public void SaveItems()
    {
        _saveSetup.coins = Items.ItemManager.Instance.GetItemByType(Items.ItemType.COIN).soInt.value;
        _saveSetup.healthPacks = Items.ItemManager.Instance.GetItemByType(Items.ItemType.LIFE_PACK).soInt.value;
        Save();
    }
    
    #endregion


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
    public int lastLevel;
    public int coins;
    public int healthPacks;
    public string playerName;
}
