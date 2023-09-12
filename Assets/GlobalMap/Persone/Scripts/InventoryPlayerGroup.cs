using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventorySaveData
{
    public List<SlotItem> SlotsItem;
}


public class InventoryPlayerGroup : SingletonBase<InventoryPlayerGroup>
{
    [SerializeField] private List<SlotItem> _slotsItem;

    public List<SlotItem> SlotsItem { get => _slotsItem; private set => _slotsItem = value; }

    private void Start()
    {
        SaveManager.Instance.InventoryPlayer = this;

        if (SaveManager.Save != null)
        {
            _slotsItem = SaveManager.Save.InventoryPlayer.SlotsItem;
        }

        SlotsItem ??= new List<SlotItem>();
    }
    //Debug Test
    public void AddAllItemsInData()
    {
        for (int i = 0; i < ItemsData.Instance.Items.Length; i++)
        {
            AddItem(ItemsData.Instance.GetItem((ItemID)i), 1);
        }
    }

    /// <summary>
    /// получить ссылку на слот с предметом в инвенторе по совподению ItemBase
    /// </summary>
    /// <param name="name"></param>
    /// <returns>SlotInventory</returns>
    public SlotItem GetSlot(ItemBase item)
    {
        if (SlotsItem.Count == 0) return null;

        var _item = SlotsItem.Find(x => x.Item == item);
        return _item;
    }
    /// <summary>
    /// добавить новый предмет (происходит создание нового слота) 
    /// или добавляет количество предметов
    /// </summary>
    /// <param name="item"></param>
    /// <param name="count"></param>
    public void AddItem(ItemBase item, int count)
    {
        var itemInList = GetSlot(item);
        if (itemInList == null)
        {
            var slot = new SlotItem(item, count);
            SlotsItem.Add(slot);
        }
        else
        {
            itemInList.Count += count;
        }
    }
    /// <summary>
    /// удаляет предмет если его количество 0 или возвращает false (происходит создание нового слота) 
    /// или убавляет количество предметов
    /// </summary>
    /// <param name="item"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public bool RemoveItem(ItemBase item, int count)
    {
        var itemInList = GetSlot(item);
        if (itemInList == null || itemInList.Count - count < 0)
        {
            return false;
        }
        else if (itemInList.Count - count == 0)
        {
            itemInList.Count -= count;
            SlotsItem.Remove(itemInList);
            return true;
        }
        else
        {
            itemInList.Count -= count;
            return true;
        }
    }

    public InventorySaveData GetInventoryData()
    {
        var data = new InventorySaveData
        {
            SlotsItem = SlotsItem
        };
        return data;
    }
}
