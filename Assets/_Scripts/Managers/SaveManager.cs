using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArreTools.Singleton;
using Items;

public class SaveManager : Singleton<SaveManager>
{
    public ItemManager itemManager { get; private set; }
    public int lastLevel;
    public Action<SaveSetup> FileLoaded;

    [SerializeField] private SaveSetup _saveSetup;
    
    public SaveSetup saveSetup
    { 
        get { return _saveSetup; } 
    }
    private string _path = Application.dataPath + "/save.txt";

    protected override void Awake()
    {
        base.Awake();

        transform.SetParent(null);
        DontDestroyOnLoad(gameObject);

    }
    
    private void CreateNewSave()
    {
        _saveSetup = new SaveSetup();
        _saveSetup.lastLevel = 0;
        _saveSetup.playerName = "arre";
    }

    void Start()
    {
        Invoke(nameof(Load), .1f);
    }

    void Update()
    {
        GetItemManager();    
    }

    private void GetItemManager()
    {
        if (itemManager == null) itemManager = GameObject.Find("ItemManager").GetComponent<ItemManager>();
    }

    #region SAVE
    [NaughtyAttributes.Button]
    public void Save()
    {
        string setupToJson = JsonUtility.ToJson(_saveSetup, true);
        //Debug.Log(setupToJson);
        SaveFile(setupToJson);
    }   

    public void SaveLastLevel(int level)
    {
        Debug.Log("EAI CRL");
        _saveSetup.lastLevel = level;
        SaveItems();
        Save();
    }

    public void SaveName(string text)
    {
        _saveSetup.playerName = text;
        Save();
    }

    public void SaveItems()
    {
        Debug.Log(itemManager.CoinAmount() + "Coins" + itemManager.HealthPackAmount() + "HealthPacks");
        _saveSetup.coinAmount = itemManager.CoinAmount();
        _saveSetup.healthPackAmount = itemManager.HealthPackAmount();
        Save();
    }

    public void SaveLastCheckpoint(int key)
    {
        _saveSetup.lastCheckpoint = key;
        Save();
    }

    public void SaveCurrentHp(int hp)
    {
        _saveSetup.currentHp = hp;
        Save();
    }
    #endregion
    private void SaveFile(string json)
    {

        Debug.Log(_path);
        File.WriteAllText(_path, json);
    }

    [NaughtyAttributes.Button]
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


        FileLoaded.Invoke(_saveSetup);
    }
}

[System.Serializable]
public class SaveSetup
{
    public int lastLevel;
    public int lastCheckpoint;
    public string playerName;
    public int currentHp;
    public int coinAmount;
    public int healthPackAmount;
}
