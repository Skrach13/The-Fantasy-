using UnityEngine;

public class SlotInventory
{
    public SlotInventory(ItemBase item, int count)
    {
        _item = item;
        _count = count;
    }
    private ItemBase _item;
    private int _count;

    public ItemBase Item { get => _item; set => _item = value; }
    public int Count { get => _count; set => _count = value; }
}
