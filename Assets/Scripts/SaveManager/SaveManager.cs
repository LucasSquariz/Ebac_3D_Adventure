using System;
using NaughtyAttributes;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;

public class SaveManager : Singleton<SaveManager>
{
    [SerializeField] private SaveSetup _saveSetup;
    private string _path;
    public int lastLevel;

    public Action<SaveSetup> FileLoaded;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this.gameObject);
        _path = Application.persistentDataPath + "/save.txt";
        
    }

    private void Start()
    {
        Load();
    }

    private void CreateNewSave()
    {
        _saveSetup = new SaveSetup();
        _saveSetup.lastLevel = 0;
        _saveSetup.playerName = "Lucas";
    }

    public SaveSetup Setup { get { return _saveSetup; }}

    #region SAVE
    [Button]
    public void Save()
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


    private void SaveFile(string json)
    {   
        File.WriteAllText(_path, json);        
    }

    [Button]
    private void Load()
    {
        string fileLoaded = "";
        if (File.Exists(_path)) 
        {
            fileLoaded = File.ReadAllText(_path);
            _saveSetup = JsonUtility.FromJson<SaveSetup>(fileLoaded);
            lastLevel = _saveSetup.lastLevel;
        }
        else
        {
            CreateNewSave();
            Save();
        }       

        FileLoaded?.Invoke(_saveSetup);
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
