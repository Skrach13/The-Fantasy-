using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/ItemBase", order = 2)]
public class ItemBase : ScriptableObject
{
    [SerializeField] private EItemID _itemID;
    [SerializeField] private Sprite _sprite;
    [TextArea(minLines:0,maxLines:20)] private string _description;

    private int _count;
    public Sprite SpriteImage { get => _sprite; set => _sprite = value; }
    public int Count { get => _count; set => _count = value; }

   
}
