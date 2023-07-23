using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/ItemBase")]
public class ItemBase : ScriptableObject
{
    [SerializeField] private ItemType _itemType;
    [SerializeField] private ItemID _itemID;
    [SerializeField] private Sprite _sprite;
    [TextArea(minLines:0,maxLines:20)] private string _description;   
    public ItemType ItemType { get => _itemType; set => _itemType = value; }
    public ItemID ItemID { get => _itemID; set => _itemID = value; }
    public Sprite SpriteImage { get => _sprite; set => _sprite = value; }
}
