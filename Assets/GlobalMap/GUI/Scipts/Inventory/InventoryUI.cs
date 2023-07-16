using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private SlotItemUI _slotUIPrefab;
    [SerializeField] private ItemUI _itemUIPrefab;
    [SerializeField] private int _additionalSlots;

    private List<SlotItemUI> _slotsUI = new List<SlotItemUI>();


    [SerializeField] private GameObject _container;

    private void OnEnable()
    {
        AddItemsAndSlots();
    }

    public void AddItemsAndSlots()
    {
        if (_slotsUI.Count != 0)
        {
            for (int i = 0; i < _slotsUI.Count; i++)
            {
                Destroy(_slotsUI[i].gameObject);              
            }
                _slotsUI.Clear();
        }
        for (int i = 0; i < InventoryPlayerGroup.Instance.SlotsItem.Count + _additionalSlots; i++)
        {
            _slotsUI.Add(Instantiate(_slotUIPrefab, _container.transform));
            if (i < InventoryPlayerGroup.Instance.SlotsItem.Count)
            {
               ItemUI itemUI = Instantiate(_itemUIPrefab, _slotsUI[i].transform);
                _slotsUI[i].ItemUI = itemUI;                
                //debug test
                //TODO
                itemUI.Item = ItemsData.Instance.GetItem((ItemID)i);
                itemUI.name = itemUI.Item.name;
                itemUI.SetSprite();
            }
        }
    }

    public void DebugAddItems()
    {
        AddItemsAndSlots();
    }
}
