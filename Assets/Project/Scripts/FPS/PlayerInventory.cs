using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInventory : MonoBehaviour
{
    public Action OnInventoryChanged;

    [Header("Item settings")]
    [SerializeField] private Transform _gunsPoint;

    public Dictionary<ItemType, ItemData> ItemsData => _itemData;
    public ItemType CurrentWeapon => _equippedType;

    private Dictionary<ItemType, int> _inventory = new Dictionary<ItemType, int>();
    private Dictionary<ItemType, ItemData> _itemData = new Dictionary<ItemType, ItemData>();
    private Dictionary<ItemType, GameObject> _spawnedGuns = new Dictionary<ItemType, GameObject>();

    private GameObject _activeItem;
    private ItemType _equippedType = ItemType.None;

    public void Hotbar(InputAction.CallbackContext callbackContext)
    {
        if (!callbackContext.started)
            return;

        int chosenItem = callbackContext.action.GetBindingIndexForControl(callbackContext.control);

        switch (chosenItem)
        {
            case 0:
                Equip(ItemType.Rifle);
                break;
            case 1:
                Equip(ItemType.Pistol);
                break;
        }
    }

    public void AddInventoryItem(ItemType type, GameObject prefab, Sprite icon, int count)
    {
        if (_inventory.ContainsKey(type))
            _inventory[type] += count;
        else
            _inventory.Add(type, count);

        if (prefab != null && icon != null && !_itemData.ContainsKey(type))
        {
            ItemData data = new ItemData();
            data.itemPrefab = prefab;
            data.itemIcon = icon;

            _itemData.Add(type, data);
            Debug.Log($"{_itemData[type].itemPrefab}, {_itemData[type].itemIcon}");
        }

        OnInventoryChanged?.Invoke();
    }

    public void RemoveInventoryItem(ItemType type, int count)
    {
        if (!_inventory.ContainsKey(type))
            return;

        _inventory[type] -= count;

        if (_inventory[type] <= 0)
            _inventory.Remove(type);

        OnInventoryChanged?.Invoke();
    }

    public bool HaveItem(ItemType type, out int count)
    {
        return _inventory.TryGetValue(type, out count);
    }

    private void Equip(ItemType type)
    {
        if (!_inventory.ContainsKey(type) || _equippedType == type)
            return;

        if (_activeItem != null)
            _activeItem.SetActive(false);

        if (_spawnedGuns.TryGetValue(type, out GameObject existingGun))
        {
            existingGun.SetActive(true);
            _activeItem = existingGun;
        }
        else if (_itemData.ContainsKey(type))
        {
            GameObject createdGun = Instantiate(_itemData[type].itemPrefab, _gunsPoint.position, _gunsPoint.rotation, _gunsPoint);
            _spawnedGuns.Add(type, createdGun);
            _activeItem = createdGun;
        }       

        _equippedType = type;
        OnInventoryChanged?.Invoke();
    }
}

[Serializable]
public class ItemData
{
    public GameObject itemPrefab;
    public Sprite itemIcon;
}