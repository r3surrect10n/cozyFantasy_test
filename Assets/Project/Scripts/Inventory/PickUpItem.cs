using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    [SerializeField] private ItemType type;
    [SerializeField] private Sprite icon;
    [SerializeField] private int count = 1;

    public ItemType Type => type;
    public Sprite Icon => icon;
    public int Count => count;
}
