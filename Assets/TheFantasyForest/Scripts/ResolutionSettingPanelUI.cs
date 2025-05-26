using TMPro;
using UnityEngine;

public class ResolutionSettingPanelUI : MonoBehaviour
{
    [SerializeField]
    private Vector2Int[] _avaibleResolution = new Vector2Int[]
   {
        new Vector2Int(800, 600),
        new Vector2Int(1280, 720),
        new Vector2Int(1600, 900),
        new Vector2Int(1920, 1080),
   };

    [SerializeField] private TextMeshProUGUI _resolutionText;

    private int _currentResolutionIndex = 3;
    public bool isMinValue { get => _currentResolutionIndex == 0; }
    public bool isMaxValue { get => _currentResolutionIndex == _avaibleResolution.Length - 1; }

    private void Start()
    {
        var resolution = SaveManager.Instance.GlobalSave.SettingsDataSave.ScreenResolution;
        Screen.SetResolution(resolution.x, resolution.y, true);
        SetText(resolution);
    }

    public void SetNextValue()
    {
        if (isMaxValue == false)
        {
            _currentResolutionIndex++;
            Apply();
        }
    }
    public void SetPreviusValue()
    {
        if (isMinValue == false)
        {
            _currentResolutionIndex--;
            Apply();
        }
    }

    private void SetText(Vector2Int value)
    {
        _resolutionText.text = $"{value.x}X{value.y}";
    }

    public void Apply()
    {
        Screen.SetResolution(_avaibleResolution[_currentResolutionIndex].x, _avaibleResolution[_currentResolutionIndex].y, true);
        SetText(_avaibleResolution[_currentResolutionIndex]);
        SaveManager.Instance.GlobalSave.SettingsDataSave.ScreenResolution = _avaibleResolution[_currentResolutionIndex];
    }
}
