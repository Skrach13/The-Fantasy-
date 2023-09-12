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
    public SettingsDataSave SettingsDataSave;
    public List<string> NameSave;    
    public GlobalSave(SettingsDataSave settingsDataSave)
    {
        NameSave = new List<string>();
        SettingsDataSave = settingsDataSave;
    }
}

[Serializable]
class  SettingsDataSave
{
    public float MasterVolume;
    public float MusicVolume;
    public float EffectVolume;
    public float EffectUIVolume;

    public Vector2Int ScreenResolution;

    public int QualityIndex;

    public float GetValueVolumeSound(NamePropertiesSoundVolume name)
    {
        switch (name)
        {
            case NamePropertiesSoundVolume.Master: return MasterVolume;
            case NamePropertiesSoundVolume.Music: return MusicVolume;
            case NamePropertiesSoundVolume.Effect: return EffectVolume;
            case NamePropertiesSoundVolume.EffectUI: return EffectUIVolume;
        }
        return 0f;
    }
    public void SetValueVolume(NamePropertiesSoundVolume name, float value)
    {
        switch (name)
        {
            case NamePropertiesSoundVolume.Master: MasterVolume = value; break;
            case NamePropertiesSoundVolume.Music: MusicVolume = value; break;
            case NamePropertiesSoundVolume.Effect: EffectVolume = value; break;
            case NamePropertiesSoundVolume.EffectUI: EffectUIVolume = value; break;
        }
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
    [SerializeField] private VolumeSettings _defaultSetting;

    [SerializeField] private GroupGlobalMap _groupGlobalMap;
    [SerializeField] private InventoryPlayerGroup _inventoryPlayer;
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
    public GroupGlobalMap GroupGlobalMap { get => _groupGlobalMap; set => _groupGlobalMap = value; }
    public InventoryPlayerGroup InventoryPlayer { get => _inventoryPlayer; set => _inventoryPlayer = value; }
    public PlayerGroupOnTheMap PlayerGroupOnTheMap { get => _playerGroupOnTheMap; set => _playerGroupOnTheMap = value; }
    public EnemyGroupsManager EnemyGroupsManager { get => _enemyGroupsManager; set => _enemyGroupsManager = value; }
    internal GlobalSave GlobalSave { get => _globalSave;private set => _globalSave = value; }

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
            SettingsDataSave settingsData = new SettingsDataSave() {

                MasterVolume = _defaultSetting.MasterVolume,
                MusicVolume = _defaultSetting.MusicVolume,
                EffectVolume = _defaultSetting.EffectVolume,
                EffectUIVolume = _defaultSetting.EffectUIVolume,
                QualityIndex = QualitySettings.names.Length - 1,

                ScreenResolution = new Vector2Int(1920,1080)
            };
            
            _globalSave = new GlobalSave(settingsData);
        }    
        
        //TEST
        Load();

    }
    private void Update()
    {
        //TEST
        if(Input.GetKeyDown(KeyCode.F5))
        {
            Debug.Log("SaveGlabalData()");
            SaveData();
        }
    }
    private void OnDestroy()
    {        
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
        SavedData group = new(_groupGlobalMap.GetSaveGroup(), _inventoryPlayer.GetInventoryData(), _playerGroupOnTheMap.GetSaveDataPlayerGroup(),_enemyGroupsManager.GetSaveDataEnemyGroups());

        Saver<SavedData>.Save(SaveName, group);
    }

    public void SaveGlabalData()
    {
        Saver<GlobalSave>.Save(GlobalSaveName, _globalSave);
    }

}



