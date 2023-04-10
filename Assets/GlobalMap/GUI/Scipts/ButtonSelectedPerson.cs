using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// скрипт прослойка для StatsUI
/// </summary>
public class ButtonSelectedPerson : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    public event Action<string> OnSelectedPerson;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(SelectedPersone);
    }
    private void SelectedPersone()
    {
        OnSelectedPerson(_text.text);
    }
}
