using System;
using UnityEngine;

[Serializable]
public class SlotItem
{
    public SlotItem(){}
    public SlotItem(ItemBase item, int count)
    {
        Item = item;
        Count = count;
    }

    public ItemBase Item;
    public int Count;

    public void AddItemAndCount(ItemBase item, int count)
    {
        Item = item;
        Count = count;
    }
}
