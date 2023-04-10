using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private SlotItemUI _slotUIPrefab;
    [SerializeField] private ItemUI _itemUIPrefab;
    [SerializeField] private int _additionalSlots;

    private List<SlotItemUI> _slotsUI = new List<SlotItemUI>();


    [SerializeField] private GameObject _container;
    private InventoryPlayerGroup _inventory;


    private void Awake()
    {
        _inventory = FindAnyObjectByType<InventoryPlayerGroup>();
    }
    private void Start()
    {
        AddItemsAndSlots();        
    }

    private void OnEnable()
    {
        
    }

    private void AddItemsAndSlots()
    {
        for(int i = 0;i < _inventory.SlotsItem.Count + _additionalSlots; i++)
        {            
            _slotsUI.Add(Instantiate(_slotUIPrefab,_container.transform));
            if(i < _inventory.SlotsItem.Count)
            {
                Instantiate(_itemUIPrefab, _slotsUI[i].transform);
            }
        }
    }
}
