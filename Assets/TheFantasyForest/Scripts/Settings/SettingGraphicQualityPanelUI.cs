using TMPro;
using UnityEngine;

public class SettingGraphicQualityPanelUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textQuality;
    private int _currentLevelIndex = 0;
    public  bool isMinValue { get => _currentLevelIndex == 0; }
    public  bool isMaxValue { get => _currentLevelIndex == QualitySettings.names.Length - 1; }

    private void Start()
    {
        _currentLevelIndex = SaveManager.Instance.GlobalSave.SettingsDataSave.QualityIndex;
        Apply();
    }
    public  void SetNextValue()
    {
        if (isMaxValue == false)
        {
            _currentLevelIndex++;
            Apply();
        }
    }
    public  void SetPreviusValue()
    {
        if (isMinValue == false)
        {
            _currentLevelIndex--;
            Apply();
        }
    }

    private void SetText()
    {
        _textQuality.text = QualitySettings.names[_currentLevelIndex].ToString();
    }

    public  void Apply()
    {
        QualitySettings.SetQualityLevel(_currentLevelIndex);
        SetText();
        SaveManager.Instance.GlobalSave.SettingsDataSave.QualityIndex = _currentLevelIndex;
    }
}
