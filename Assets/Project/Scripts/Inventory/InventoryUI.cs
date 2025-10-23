using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [Header("Weapons UI")]
    [SerializeField] private List<WeaponSlot> _weapons = new List<WeaponSlot>();

    [Header("Resources UI")]
    [SerializeField] private List<ResourceSlot> _resources = new List<ResourceSlot>();

    private PlayerInventory _playerInventory;

    private void Start()
    {
        _playerInventory = FindFirstObjectByType<PlayerInventory>();
        _playerInventory.OnInventoryChanged += UpdateUI;
        UpdateUI();
    }

    private void OnDestroy()
    {
        if (_playerInventory != null)
            _playerInventory.OnInventoryChanged -= UpdateUI;
    }

    private void UpdateUI()
    {
        foreach (var weapon in _weapons)
        {
            bool hasWeapon = _playerInventory.HaveItem(weapon.weaponType, out _);
            weapon.weaponImage.enabled = hasWeapon;

            if (hasWeapon && _playerInventory.ItemsData.TryGetValue(weapon.weaponType, out var data))
                weapon.weaponImage.sprite = data.itemIcon;

            weapon.selectedOutline.SetActive(_playerInventory.CurrentWeapon == weapon.weaponType);
        }

        foreach (var resource in _resources)
        {
            _playerInventory.HaveItem(resource.itemType, out int count);
            resource.count.text = count.ToString();

            if (count > 0 && _playerInventory.ItemsData.TryGetValue(resource.itemType, out var data))
            {
                resource.itemImage.sprite = data.itemIcon;
                resource.itemImage.enabled = true;
                resource.count.enabled = true;
            }
            else
            {
                resource.itemImage.enabled = false;
                resource.count.enabled = false;
            }
        }
    }
}

[Serializable]
public class WeaponSlot
{
    public ItemType weaponType;
    public Image weaponImage;
    public GameObject selectedOutline;
}

[Serializable]
public class ResourceSlot
{
    public ItemType itemType;
    public Image itemImage;
    public Text count;
}
