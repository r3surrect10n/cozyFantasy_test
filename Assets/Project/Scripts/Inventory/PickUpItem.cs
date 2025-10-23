using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    [SerializeField] private ItemType _type;
    [SerializeField] private GameObject _visiblePrefab;
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _count = 1;

    public ItemType Type => _type;
    public GameObject Prefab => _visiblePrefab;
    public Sprite Icon => _icon;
    public int Count => _count;
}
