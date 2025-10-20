using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent (typeof(RaycastHandler))]
public class PlayerShooter : MonoBehaviour
{
    public event Action<bool> OnAim;    

    [Header("Grenade throw settings")]
    [SerializeField] private Grenade _grenade;
    [SerializeField] private Transform _throwPoint;
    [SerializeField, Range (0, 60)] private float _throwAngle = 15f;
    [SerializeField, Range (0, 20)] private float _throwPower = 5f;

    private IShootable _shooter;

    private RaycastHandler _raycastHandler;        

    private bool _isAiming = false;

    private void Awake()
    {
        _raycastHandler = GetComponent<RaycastHandler>();
    }

    public void Aiming(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.started && !_isAiming)
        {
            AimState();
        }
        else if (callbackContext.canceled && _isAiming)
        {
            AimState();
        }
    }

    public void Shoot(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.started && GetComponentInChildren<IShootable>() != null)
        {
            _shooter = GetComponentInChildren<IShootable>();
            _shooter.Shoot(_raycastHandler);            
        }

        if (callbackContext.canceled)
        {
            _shooter.StopShooting();
            _shooter = null;
        }
    }

    public void Grenade(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.started && !_isAiming)
        {
            Grenade newGrenade = Instantiate(_grenade, _throwPoint.position, Quaternion.identity);
            Quaternion upperAngle = Quaternion.Euler(-_throwAngle, Vector3.zero.y, Vector3.zero.z);
            Vector3 throwDirection = _raycastHandler.CameraTransform.rotation * upperAngle * Vector3.forward;

            newGrenade.GrenadeInitialize(_throwPower, throwDirection);
        }
    }

    private void AimState()
    {
        _isAiming = !_isAiming;
        OnAim?.Invoke(_isAiming);
    }    
}
