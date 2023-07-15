using System.Collections.Generic;
using UnityEngine;

public class InventoryPlayerGroup : SingletonBase<InventoryPlayerGroup>
{
    private List<SlotInventory> _slotsItem;

    public List<SlotInventory> SlotsItem { get => _slotsItem; private set => _slotsItem = value; }

    private new void Awake()
    {
        base.Awake();
        if (SlotsItem == null)
        {
            SlotsItem = new List<SlotInventory>();
        }
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
    public SlotInventory GetSlot(ItemBase item)
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
            var slot = new SlotInventory(item, count);
            SlotsItem.Add(slot);
        }
        else
        {
            itemInList.Count += count;
        }
    }
}
