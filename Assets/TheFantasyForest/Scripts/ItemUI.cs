using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ItemUI : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] ItemBase _item;
    private CanvasGroup _canvasGroup;
    private RectTransform _rectTransform;
    private Canvas _mainCanvas;
    private Image _image;

    private Transform _parantSlotTransform;

    public Transform ParantSlotTransform { get => _parantSlotTransform; set => _parantSlotTransform = value; }
    public ItemBase Item { get => _item; set => _item = value; }

    private void Awake()
    {
        _image = GetComponent<Image>();
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _mainCanvas = GetComponentInParent<Canvas>();
        ParantSlotTransform = transform.parent.transform;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(_mainCanvas.transform);
        transform.SetAsLastSibling();
        _canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _mainCanvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        var parent = transform.parent?.GetComponent<SlotItemUI>();
        var prevParent = _parantSlotTransform.GetComponent<SlotItemPersonePanelUI>();
        if (parent == null)
        {
            transform.SetParent(ParantSlotTransform);
        }
        else
        {
            if (parent.ItemUI == null)
            {
                if (parent is SlotItemPersonePanelUI && (parent as SlotItemPersonePanelUI).TryTypeItem(Item.ItemType))
                {
                    (parent as SlotItemPersonePanelUI).SetItemPersone(Item);
                    if (prevParent is SlotItemPersonePanelUI)
                    {
                        prevParent.SetItemPersone(null);
                    }

                    if (parent is SlotItemPersonePanelUI && (prevParent is SlotItemPersonePanelUI) == false)
                    {
                        Debug.Log("remove item");
                        InventoryPlayerGroup.Instance.RemoveItem(Item, 1);
                    }
                   
                    DropItem(parent);
                }
                else if (parent is SlotItemPersonePanelUI && (parent as SlotItemPersonePanelUI).TryTypeItem(Item.ItemType) == false)
                {
                    transform.SetParent(ParantSlotTransform);
                }
                else
                {
                    if (parent is SlotItemPersonePanelUI == false && (prevParent is SlotItemPersonePanelUI) == true)
                    {
                        Debug.Log("add item");
                        prevParent.SetItemPersone(null);
                        InventoryPlayerGroup.Instance.AddItem(Item, 1);
                    }
                    DropItem(parent);
                }
            }
            else
            {
                if (parent is SlotItemPersonePanelUI && (parent as SlotItemPersonePanelUI).TryTypeItem(Item.ItemType))
                {
                    var prevItem = parent.ItemUI.Item;
                    (parent as SlotItemPersonePanelUI).SetItemPersone(Item);
                    if (prevParent is SlotItemPersonePanelUI)
                    {
                        prevParent.SetItemPersone(prevItem);
                    }
                    if (parent is SlotItemPersonePanelUI && (prevParent is SlotItemPersonePanelUI) == false)
                    {
                        Debug.Log("remove item");
                        InventoryPlayerGroup.Instance.RemoveItem(Item, 1);
                    }                 

                    SwitchItems(parent);
                }
                else if (parent is SlotItemPersonePanelUI && (parent as SlotItemPersonePanelUI).TryTypeItem(Item.ItemType) == false)
                {
                    transform.SetParent(ParantSlotTransform);
                }
                else
                {
                    if (parent is SlotItemPersonePanelUI == false && (prevParent is SlotItemPersonePanelUI) == true)
                    {
                        Debug.Log("add item");
                        InventoryPlayerGroup.Instance.AddItem(Item, 1);
                    }
                    SwitchItems(parent);
                }
            }
        }
        transform.localPosition = Vector3.zero;

        _canvasGroup.blocksRaycasts = true;
    }

    private void DropItem(SlotItemUI parent)
    {
        parent.ItemUI = this;
        _parantSlotTransform.GetComponent<SlotItemUI>().ItemUI = null;
        _parantSlotTransform = parent.transform;
    }

    private void SwitchItems(SlotItemUI parent)
    {
        //���� � ����� ���� �������          
        ItemUI itemUI = parent.ItemUI;//�������� ��������� ��� �������    
        parent.ItemUI = this; //���������� � ����� ���� ��������������� �������
        itemUI.transform.SetParent(_parantSlotTransform);//��������� ������������ ���������� ����������� �������� 
        itemUI.ParantSlotTransform = _parantSlotTransform;//���������� ����������� �������� ��� ������������ ���������
        _parantSlotTransform.GetComponent<SlotItemUI>().ItemUI = itemUI;//���������� � ���� �� �������� ������������� ���������� ������� 
        itemUI.transform.localPosition = Vector3.zero;//������������� ������� ����������� �������� � ���� ���������������� ��������
        _parantSlotTransform = parent.transform;//��������� ����������� ���������� ���������������� ��������
    }

    public void SetSprite()
    {
        _image.sprite = _item.SpriteImage;
    }

}
