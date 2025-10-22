using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [Header("Weapons")]
    [SerializeField] private List<WeaponSlot> _weapons = new List<WeaponSlot>();

    [Header("Resources")]
    [SerializeField] private List<ResourceSlot> _resources = new List<ResourceSlot>();

    private PlayerInventory _playerInventory;

    private void Start()
    {
        _playerInventory = FindFirstObjectByType<PlayerInventory>();
        _playerInventory.OnInventoryChanged += UpdateUI;
    }

    private void OnDestroy()
    {
        _playerInventory.OnInventoryChanged -= UpdateUI;
    }

    private void UpdateUI()
    {
        
    }
}

[Serializable]
public class WeaponSlot
{
    public ItemType weaponType;
    public GameObject weaponImage;
}

[Serializable]
public class ResourceSlot
{
    public ItemType itemType;
    public GameObject itemPanel;
    public GameObject itemImage;
    public Text count;
}
