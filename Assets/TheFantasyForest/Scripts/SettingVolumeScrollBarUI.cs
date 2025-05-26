using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingVolumeScrollBarUI : MonoBehaviour
{ 
    [SerializeField] private NamePropertiesSoundVolume _nameProperties;

    [SerializeField] private AudioMixerGroup _mixerGroup;
    [SerializeField] private Scrollbar _scrollbar;

    [SerializeField] private float _minRealValue;
    [SerializeField] private float _maxRealValue;
   
    [SerializeField] private float _minVirtualValue;
    [SerializeField] private float _maxVirtualValue;

    private void Start()
    {
        float value = SaveManager.Instance.GlobalSave.SettingsDataSave.GetValueVolumeSound(_nameProperties);       
        _mixerGroup.audioMixer.SetFloat(_mixerGroup.name, value);
        _scrollbar.value = Mathf.Lerp(_minVirtualValue, _maxVirtualValue, (value - _minRealValue) / (_maxRealValue - _minRealValue));
        _scrollbar.onValueChanged.AddListener(OnChangeValue);
    }
    private void OnDestroy()
    {
        _scrollbar.onValueChanged.RemoveListener(OnChangeValue);
    }

    private void OnChangeValue(float value)
    {
        float valueVolumeSound = Mathf.Lerp(_minRealValue, _maxRealValue, value);
        _mixerGroup.audioMixer.SetFloat(_mixerGroup.name, valueVolumeSound);
        SaveManager.Instance.GlobalSave.SettingsDataSave.SetValueVolume(_nameProperties, valueVolumeSound);
        
    }    

}
