using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Action OnInventoryChanged;

    private Dictionary<ItemType, int> _inventory = new Dictionary<ItemType, int>();    

    public void AddInventoryItem(ItemType type, int count)
    {
        if (_inventory.ContainsKey(type))
            _inventory[type] += count;
        else        
            _inventory.Add(type, count);        
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
}
