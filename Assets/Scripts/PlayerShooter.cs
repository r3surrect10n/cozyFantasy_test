using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooter : MonoBehaviour
{
    public event Action<bool> OnAim;

    private bool _isAim = false;

    public void PlayerAim(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.started && !_isAim)        
            AimingState();        
        else if (callbackContext.canceled)        
            AimingState();        
    }

    private void AimingState()
    {
        _isAim = !_isAim;        
        OnAim?.Invoke(_isAim);
    }
}
