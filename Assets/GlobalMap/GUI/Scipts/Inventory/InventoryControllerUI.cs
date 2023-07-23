using System;
using UnityEngine;

public class InventoryControllerUI : MonoBehaviour
{
    [SerializeField] private SelectedPersonePanel _selectedPersone;
    [SerializeField] private InventoryUI _inventoryUI;
    [SerializeField] private PersonePanelUI _personePanel;

    private string _nameSelectedPersone;

    public string NameSelectedPersone { get => _nameSelectedPersone; set => _nameSelectedPersone = value; }

    private void Start()
    {
        _selectedPersone.OnSelectedPerson += SelectedPersone;
    }

    private void SelectedPersone(string name)
    {
        _inventoryUI.AddItemsAndSlots();
        _personePanel.UpddatePanel(GroupGlobalMap.Instance.GetPerosne(name));
        NameSelectedPersone = name;
    }

    private void OnEnable()
    {
        NameSelectedPersone = GroupGlobalMap.Instance.Group[0].Name;
        SelectedPersone(NameSelectedPersone);        
    }
}
