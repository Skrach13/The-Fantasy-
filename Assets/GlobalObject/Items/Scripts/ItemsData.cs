using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemsData : SingletonBase<ItemsData>
{

    [SerializeField] private ItemBase[] _items;

    public ItemBase[] Items { get => _items;private set => _items = value; }

    public ItemBase GetItem(ItemID ItemID)
    {
        ItemBase itemBase = Items.First(x => x.ItemID == ItemID);
        return itemBase;
    }
}
