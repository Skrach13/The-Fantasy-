using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotItemPersonePanelUI : SlotItemUI, IDropHandler
{
    public enum SlotPersoneType
    {
        RightHanded,
        LeftHanded,
        Body,
        Necklece,
        Ring,
        Potion
    }

    [SerializeField] private PersonePanelUI _personePanelUI;
    [SerializeField] private SlotPersoneType _slotType;

    private PlayerPersone _playerPersone => _personePanelUI.PersoneSelect;
    public SlotPersoneType SlotType { get => _slotType; set => _slotType = value; }

    public void SetItemPersone(ItemBase value)
    {
        //TODO возможно надо поработать с количеством предметов   
        if (_slotType == SlotPersoneType.RightHanded)
        {
            if (value == null)
            {
                Debug.Log("del itemdsasfasdf");
                if (_playerPersone.TrySkill((_playerPersone.RightHandItem.Item as ItemWeapone).SkillAttacking.KeySkill))
                {
                    var skilldel = _playerPersone.Skills.Find(skill => skill.KeySkill == (_playerPersone.RightHandItem.Item as ItemWeapone).SkillAttacking.KeySkill);
                   _playerPersone.Skills.Remove(skilldel);
                }
                _playerPersone.RightHandItem.Item = null;
                _playerPersone.RightHandItem.Count = 0;
                if (_playerPersone.Skills.Count != 0)
                {
                    foreach (SkillBase skills in _playerPersone.Skills)
                    {
                        Debug.Log($"{skills.Name}");

                    }
                }
                else
                {
                        Debug.Log($"_playerPersone.Skills.Count == 0");
                }
            }
            else
            {
                _playerPersone.RightHandItem.AddItemAndCount(value, 1);
                if (!_playerPersone.TrySkill((value as ItemWeapone).SkillAttacking.KeySkill))
                {
                    _playerPersone.Skills.Add((value as ItemWeapone).SkillAttacking);
                }

                foreach (SkillBase skills in _playerPersone.Skills)
                {
                    Debug.Log($"{skills.Name}");

                }
            }
        }
        if (_slotType == SlotPersoneType.LeftHanded)
        {
            if (value == null)
            {
                _playerPersone.LeftHandItem.Item = null;
                _playerPersone.LeftHandItem.Count = 0;
            }
            else
            {
                _playerPersone.LeftHandItem.AddItemAndCount(value, 1);
            }
        }
        if (_slotType == SlotPersoneType.Body)
        {
            if (value == null)
            {
                _playerPersone.BodyItem.Item = null;
                _playerPersone.BodyItem.Count = 0;
            }
            else
            {
                _playerPersone.BodyItem.AddItemAndCount(value, 1);
            }
        }
        if (_slotType == SlotPersoneType.Necklece)
        {
            if (value == null)
            {
                _playerPersone.NecklaceItem.Item = null;
                _playerPersone.NecklaceItem.Count = 0;
            }
            else
            {
                _playerPersone.NecklaceItem.AddItemAndCount(value, 1);
            }
        }
        if (_slotType == SlotPersoneType.Ring)
        {
            if (value == null)
            {
                _playerPersone.RingItem.Item = null;
                _playerPersone.RingItem.Count = 0;
            }
            else
            {
                _playerPersone.RingItem.AddItemAndCount(value, 1);
            }
        }
        if (_slotType == SlotPersoneType.Potion)
        {
            if (value == null)
            {
                _playerPersone.PotionItem.Item = null;
                _playerPersone.PotionItem.Count = 0;
            }
            else
            {
                _playerPersone.PotionItem.AddItemAndCount(value, 1);
            }
        }
    }
    public bool TryTypeItem(ItemType type)
    {
        if (type == ItemType.Weapone && (SlotType == SlotPersoneType.RightHanded || SlotType == SlotPersoneType.LeftHanded))
        {
            return true;
        }
        if (type == ItemType.Armor && SlotType == SlotPersoneType.Body)
        {
            return true;
        }
        if (type == ItemType.Jewelry && (SlotType == SlotPersoneType.Necklece || SlotType == SlotPersoneType.Ring))
        {
            return true;
        }
        if (type == ItemType.Potion && SlotType == SlotPersoneType.Potion)
        {
            return true;
        }

        return false;
    }
}
