using System;
using UnityEngine;

public enum ItemType
{
    None,
    Pistol,
    Rifle,
    Grenade,
    Ammo    
}

[Serializable]
public class InventoryItem
{
    public ItemType itemType;
    public Sprite icon;
    public int count;

    public InventoryItem(ItemType itemType, int count)
    {
        this.itemType = itemType;
        this.count = count;
    }
}
