using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private List<WeaponSlot> _weapons = new List<WeaponSlot>();
    [SerializeField] private List<ResourceSlot> _resources = new List<ResourceSlot>();

    private ItemType _currentEquipped = ItemType.None;

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
            weapon.panel.SetActive(true);
            weapon.weaponImage.enabled = hasWeapon;

            if (hasWeapon && _playerInventory.ItemsData.TryGetValue(weapon.weaponType, out var data))
                weapon.weaponImage.sprite = data.itemIcon;

            weapon.selectedOutline.SetActive(weapon.weaponType == _currentEquipped);
        }

        foreach (var resource in _resources)
        {
            _playerInventory.HaveItem(resource.itemType, out int count);
            resource.count.text = count.ToString();

            if (count > 0 && !resource.panel.activeInHierarchy)
                resource.panel.SetActive(true);

            resource.itemImage.enabled = true;
        }
    }

    public void SetEquippedWeapon(ItemType type)
    {
        _currentEquipped = type;
        UpdateUI();
    }
}

[Serializable]
public class WeaponSlot
{
    public ItemType weaponType;
    public GameObject panel;
    public Image weaponImage;
    public GameObject selectedOutline;
}

[Serializable]
public class ResourceSlot
{
    public ItemType itemType;
    public GameObject panel;
    public Image itemImage;
    public Text count;
}