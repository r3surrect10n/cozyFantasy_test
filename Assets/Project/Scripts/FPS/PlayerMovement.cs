using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent (typeof(CharacterController))]
[RequireComponent (typeof(PlayerInput))]
[RequireComponent (typeof(PlayerShooter))]
public class PlayerMovement : MonoBehaviour
{
    private const float _gravity = -9.81f;

    [Header("Main camera")]
    [SerializeField] private Camera _playerCam;
    [SerializeField] private Camera _weaponCam;
    [SerializeField] private float _defaultFov = 60f;
    [SerializeField] private float _aimFov = 45f;

    [Header("Player settings")]
    [SerializeField, Range (1, 10)] private float _defaultMovementSpeed = 4f;
    [SerializeField, Range (0, 1)] private float _aimMovementMultiplier = 0.5f;
    [SerializeField, Range (0, 1)] private float _airMovementMultiplier = 0.25f;
    [SerializeField, Range (-10, 0)] private float _earthPull = -2f;
    [SerializeField, Range (0, 50)] private float _jumpHeight;

    [Header("Mouse settings")]
    [SerializeField, Range (0, 100)] private float _mouseSensivity = 25f;
    [SerializeField, Range (0, 90)] private float _maxAngle = 90f;

    private float _movementSpeed;
    private float _cameraRotation = 0f;

    private bool _isAiming = false;
    
    private Vector2 _movementInput;
    private Vector2 _lookInput;

    private Vector3 _velocity;

    private CharacterController _characterController;
    private PlayerShooter _playerShooter;    

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _playerShooter = GetComponent<PlayerShooter>();

        _movementSpeed = _defaultMovementSpeed;

        _playerCam.fieldOfView = _defaultFov;        
    }

    private void OnEnable()
    {
        _playerShooter.OnAim += AimFOV;
    }

    private void OnDisable()
    {
        _playerShooter.OnAim -= AimFOV;
    }

    private void Update()
    {
        MovementHandler();
        LookHandler();
        GravityHandler();        
    }    

    public void Move(InputAction.CallbackContext callbackContext)
    {
        _movementInput = callbackContext.ReadValue<Vector2>();
    }

    public void Look(InputAction.CallbackContext callbackContext)
    {
        _lookInput = callbackContext.ReadValue<Vector2>();
    }

    public void Jump(InputAction.CallbackContext callbackContext)
    {
        if (!callbackContext.started || !_characterController.isGrounded)
            return;

        _velocity.y = Mathf.Sqrt(_jumpHeight * _earthPull * _gravity);        
    }

    private void MovementHandler()
    {
        if (_movementInput.magnitude > 0.1f)
        {
            Vector3 direction = _movementInput.y * _playerCam.transform.forward + _movementInput.x * _playerCam.transform.right;
            direction.y = 0;
            direction.Normalize();

            _movementSpeed = _defaultMovementSpeed;

            if (_isAiming)
                _movementSpeed *= _aimMovementMultiplier;

            if (!_characterController.isGrounded)
                _movementSpeed *= _airMovementMultiplier;

            Vector3 movement = direction * _movementSpeed * Time.deltaTime;
            _characterController.Move(movement);
        }
    }

    private void LookHandler()
    {
        if (_lookInput.magnitude > 0.1f)
        {
            float mouseX = _lookInput.x * _mouseSensivity * Time.deltaTime;
            float mouseY = _lookInput.y * _mouseSensivity * Time.deltaTime;

            _cameraRotation -= mouseY;
            _cameraRotation = Mathf.Clamp(_cameraRotation, -_maxAngle, _maxAngle);
            _playerCam.transform.localRotation = Quaternion.Euler(_cameraRotation, 0, 0);

            transform.Rotate(Vector3.up * mouseX);
        }
    }

    private void GravityHandler()
    {
        if (_characterController.isGrounded && _velocity.y < 0)
            _velocity.y = _earthPull;
        else
            _velocity.y += _gravity * Time.deltaTime;

        _characterController.Move(_velocity * Time.deltaTime);
    }

    private void AimFOV(bool aiming)
    {
        if (aiming)        
            AimState(_aimFov);        
        else        
            AimState(_defaultFov);        
    }

    private void AimState(float fov)
    {
        _isAiming = !_isAiming;        

        _playerCam.fieldOfView = fov;
        _weaponCam.fieldOfView = fov;
    }
}
