using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public enum KeyNameData
{
    PlayerGroup
}


[Serializable]
class GlobalSave
{  
    public List<string> NameSave;    
    public GlobalSave()
    {
        NameSave = new List<string>();
    }
}


[Serializable]
public class SavedData
{
    public SavedPlayerPersoneGroup PlayerPersoneGroup;
    public InventorySaveData InventoryPlayer;
    public SaveDataPlayerGroupOnTheMap PlayerGroupOnTheMap;
    public SaveDataEnemyGroups EnemyGroups;
    public SavedData() { }
    public SavedData(SavedPlayerPersoneGroup savedPlayerPersone, InventorySaveData inventoryPlayer, SaveDataPlayerGroupOnTheMap playerGroupOnTheMap, SaveDataEnemyGroups enemyGroups)
    {
        PlayerPersoneGroup = savedPlayerPersone;
        InventoryPlayer = inventoryPlayer;
        PlayerGroupOnTheMap = playerGroupOnTheMap;
        EnemyGroups = enemyGroups;
    }
}

public class SaveManager : SingletonBase<SaveManager>
{
    [SerializeField] private GroupGlobalMap _groupGlobalMap;
    [SerializeField] private InventoryPlayerGroup InventoryPlayer;
    [SerializeField] private PlayerGroupOnTheMap _playerGroupOnTheMap;
    [SerializeField] private EnemyGroupsManager _enemyGroupsManager;

    private static bool isLoad = false;
    private static string _saveName = "AutoSave";
    private static string _loadName = "AutoSave";
    private const string GlobalSaveName = "GlobalSave";

    private GlobalSave _globalSave;
    private static SavedData _save;

    public static bool IsLoad { get => isLoad; private set => isLoad = value; }
    public static string SaveName { get => _saveName; private set => _saveName = value; }
    public static string LoadName { get => _loadName; private set => _loadName = value; }
    public static SavedData Save { get => _save; set => _save = value; }

    protected override void Awake()
    {
        base.Awake();
        if (Directory.Exists($"{Application.dataPath}/Save") == false)
        {
            Directory.CreateDirectory($"{Application.dataPath}/Save");
        }
        Saver<GlobalSave>.TryLoad(GlobalSaveName, ref _globalSave);
        if (_globalSave == null)
        {
            Debug.LogWarning($"_globalSave == null");
            _globalSave = new GlobalSave();
        }       
        //TEST
        Load();

    }
    private void OnDestroy()
    {
        Debug.Log("SaveGlabalData()");
        SaveData();
        SaveGlabalData();
    }
    private void Load()
    {
        var save = new SavedData();
        Saver<SavedData>.TryLoad(LoadName, ref save);
        Save = save;

    }
    private void SaveData()
    {
        if (_globalSave.NameSave.Find(name => name == SaveName) == null)
        {
            _globalSave.NameSave.Add(SaveName);
        }
        SavedData group = new(_groupGlobalMap.GetSaveGroup(), InventoryPlayer.GetInventoryData(), _playerGroupOnTheMap.GetSaveDataPlayerGroup(),_enemyGroupsManager.GetSaveDataEnemyGroups());

        Saver<SavedData>.Save(SaveName, group);
    }

    public void SaveGlabalData()
    {
        Saver<GlobalSave>.Save(GlobalSaveName, _globalSave);
    }

}



