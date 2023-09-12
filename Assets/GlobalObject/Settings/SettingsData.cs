using UnityEngine;

public class SettingsData : SingletonBase<SettingsData>
{
    public float MasterVolume;
    public float MusicVolume;
    public float EffectVolume;
    public float EffectUIVolume;

    public Vector2 ScreenResolution;
}
