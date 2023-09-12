using UnityEngine;

public enum NamePropertiesSoundVolume
{
    Master,
    Music,
    Effect,
    EffectUI
}

[CreateAssetMenu(fileName = "VolumeSetings", menuName = "Setting/VolumeSetings")]
public class VolumeSettings : ScriptableObject
{
    [Range(-80, 20)] public float MasterVolume;
    [Range(-80, 20)] public float MusicVolume;
    [Range(-80, 20)] public float EffectVolume;
    [Range(-80, 20)] public float EffectUIVolume;
}


