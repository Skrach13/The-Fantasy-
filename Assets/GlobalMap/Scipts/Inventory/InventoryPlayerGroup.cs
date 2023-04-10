using System.Collections.Generic;

public class InventoryPlayerGroup : SingletonBase<InventoryPlayerGroup>
{
    List<SlotInventory> _slotsItem;   

    private new void Awake()
    {
        base.Awake();
        if( _slotsItem == null )
        {
            _slotsItem = new List<SlotInventory>();
        }
    }

    /// <summary>
    /// получить ссылку на слот с предметом в инвенторя по совподению ItemBase
    /// </summary>
    /// <param name="name"></param>
    /// <returns>SlotInventory</returns>
    public SlotInventory GetSlot(ItemBase item)
    {       
        if (_slotsItem.Count == 0) return null;

        var _item = _slotsItem.Find(x => x.Item == item);
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
        if (itemInList == null )
        {           
            var slot = new SlotInventory(item, count);
            _slotsItem.Add(slot);
        }
        else
        {
            itemInList.Count += count;        
        }        
    }
}
