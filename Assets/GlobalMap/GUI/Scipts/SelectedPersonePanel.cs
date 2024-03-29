using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectedPersonePanel : MonoBehaviour
{    
    [SerializeField] private ButtonSelectedPerson _buttonPrefab;
    public event Action<string> OnSelectedPerson;

    private List<ButtonSelectedPerson> _selectedButtons = new List<ButtonSelectedPerson>();

    private void OnEnable()
    {        
        CreateButtomSelectedPersone();
    }
    private void OnDisable()
    {
        DeleteButtomSelectedPersone();
    }
    private void VisualStatsPersone(string name)
    {
        OnSelectedPerson?.Invoke(name);       
    }
    
    /// <summary>
    /// TODO �������� ���� ������� ��� ������ ����� �� ������� � ������ ��������� ������
    /// </summary>
    private void CreateButtomSelectedPersone()
    {
        for (int i = 0; i < PlayerGroupGlobal.Instance.Group.Count; i++)
        {
            ButtonSelectedPerson newButton = Instantiate(_buttonPrefab, transform);
            newButton.OnSelectedPerson += VisualStatsPersone;

            newButton.GetComponentInChildren<TextMeshProUGUI>().text = PlayerGroupGlobal.Instance.Group[i].Name;
            _selectedButtons.Add(newButton);
        }
    }
    /// <summary>
    /// TODO �������� ���� ������� ��� ������ ����� �� ������� � ������ ��������� ������
    /// </summary>
    private void DeleteButtomSelectedPersone()
    {
        for (int i = 0; i < _selectedButtons.Count; i++)
        {
            _selectedButtons[i].OnSelectedPerson -= VisualStatsPersone;
            Destroy(_selectedButtons[i].gameObject);
        }
        _selectedButtons.Clear();
    }
}
