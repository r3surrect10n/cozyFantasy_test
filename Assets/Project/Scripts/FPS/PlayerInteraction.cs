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

    private Collider _lookCollider;


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
        if (_lookCollider == null)
            return;

        if (callbackContext.started)
        {
            if (_lookCollider.TryGetComponent<PickUpItem>(out var item))
            {
                _playerInventory.AddInventoryItem(item.Type, item.Prefab, item.Icon, item.Count);                
                Destroy(item.gameObject);
            }
        }
    }

    private void ShowTip()
    {
        _lookCollider = _raycastHandler.InteractionHit.collider;

        if (_lookCollider != null && _lookCollider.TryGetComponent<PickUpItem>(out var _))
        {
            _interactionTip.SetActive(true);            
        }
        else if (_interactionTip.activeInHierarchy)
            _interactionTip.SetActive(false);
    }
}
