using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent (typeof(RaycastHandler))]
[RequireComponent (typeof(PlayerInventory))]
public class PlayerInteraction : MonoBehaviour
{
    [Header("UI settings")]
    [SerializeField] private GameObject _interactionTip;

    private RaycastHandler _raycastHandler;
    private PlayerInventory _playerInventory;

    private void Awake()
    {
        _raycastHandler = GetComponent<RaycastHandler>();
        _playerInventory = GetComponent<PlayerInventory>();
    }

    private void Update()
    {
        ShowTip();
    }

    public void Interact(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.started)
        {
            if (_raycastHandler.InteractionHit.collider.TryGetComponent<PickUpItem>(out var item))
            {
                _playerInventory.AddInventoryItem(item.Type, item.Icon, item.Count);
                Destroy(item);
            }
        }
    }

    private void ShowTip()
    {
        if (_raycastHandler.InteractionHit.collider != null && _raycastHandler.InteractionHit.collider.TryGetComponent<PickUpItem>(out var item))
        {
            _interactionTip.SetActive(true);            
        }
        else if (_interactionTip.activeInHierarchy)
            _interactionTip.SetActive(false);
    }
}
