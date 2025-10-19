using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(RaycastHandler))]
public class PlayerInteraction : MonoBehaviour
{
    [Header("UI settings")]
    [SerializeField] private GameObject _interactionTip;

    private RaycastHandler _raycastHandler;

    private void Awake()
    {
        _raycastHandler = GetComponent<RaycastHandler>();
    }

    private void Update()
    {
        ShowTip();
    }

    public void Interact(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.started)
        {
            if (_raycastHandler.InteractionHit.collider.TryGetComponent<IInteractable>(out var interactable))
            {

            } 
        }
    }

    private void ShowTip()
    {
        if (_raycastHandler.InteractionHit.collider != null && _raycastHandler.InteractionHit.collider.TryGetComponent<IInteractable>(out var interactable))
        {
            _interactionTip.SetActive(true);
            Debug.Log("123");
        }
        else if (_interactionTip.activeInHierarchy)
            _interactionTip.SetActive(false);
    }
}
