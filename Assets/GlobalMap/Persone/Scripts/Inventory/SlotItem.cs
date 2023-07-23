using System;
using UnityEngine;

[Serializable]
public class SlotItem
{
    public SlotItem(ItemBase item, int count)
    {
        _item = item;
        _count = count;
    }

    public SlotItem()
    {
    }

    private ItemBase _item;
    private int _count;

    public ItemBase Item { get => _item; set => _item = value; }
    public int Count { get => _count; set => _count = value; }

    public void AddItemAndCount(ItemBase item, int count)
    {
        _item = item; 
        _count = count;
    }
}
