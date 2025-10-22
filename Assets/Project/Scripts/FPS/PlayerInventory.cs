using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInventory : MonoBehaviour
{
    public Action OnInventoryChanged;    

    private Dictionary<ItemType, int> _inventory = new Dictionary<ItemType, int>();  
    private Dictionary<ItemType, Sprite> _icons = new Dictionary<ItemType, Sprite>();

    public void Hotbar(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.started)
            Equip(callbackContext.action.GetBindingIndexForControl(callbackContext.control));
    }

    public void AddInventoryItem(ItemType type, Sprite icon, int count)
    {
        if (_inventory.ContainsKey(type))
            _inventory[type] += count;
        else        
            _inventory.Add(type, count);

        if (icon != null && !_icons.ContainsKey(type))
            _icons.Add(type, icon);

        OnInventoryChanged?.Invoke();
    }

    public void RemoveInventoryItem(ItemType type, int count)
    {
        if (!_inventory.ContainsKey(type))
            return;

        _inventory[type] -= count;

        if (_inventory[type] <= 0 )
            _inventory.Remove(type);            
    }
    
    public bool HaveItem(ItemType type, out int count)
    {
        return _inventory.TryGetValue(type, out count);
    }

    private void Equip(int slotIndex)
    {
        switch (slotIndex)
        {
            case 0:
               
                break;
            case 1:
               
                break;
        }
    }

    private void ActivateModel(ItemType type)
    {
        if (!_inventory.ContainsKey(type))
            return;

        switch (type)
        {
            case ItemType.Rifle:
                break;
            case ItemType.Pistol:
                break;
        }
    }
}


