using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerShooter))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Camera _playerCam;
    [SerializeField] private GameObject _freeLookCamera;
    [SerializeField] private GameObject _aimCamera;

    [SerializeField] private float _movementDefaultSpeed;
    [SerializeField] private float _rotationSpeed;

    private CharacterController _characterController;
    private PlayerShooter _playerShooter;

    private Vector2 _movementInput;
    private Vector2 _lookInput;
    private Vector3 _lookDirection;

    private float _movementSpeed;
    private bool _isAiming;

    private void OnValidate()
    {
        _characterController = GetComponent<CharacterController>();
        _playerShooter = GetComponent<PlayerShooter>();
    }

    private void OnEnable()
    {
        _playerShooter.OnAim += AimingMovement;
    }

    private void OnDisable()
    {
        _playerShooter.OnAim -= AimingMovement;
    }

    public void Awake()
    {
        _movementSpeed = _movementDefaultSpeed;
    }


    private void Update()
    {
        OnMove();        
    }

    public void MoveInput(InputAction.CallbackContext callbackContext)
    {
        _movementInput = callbackContext.ReadValue<Vector2>();
    }
    
    public void LookInput(InputAction.CallbackContext callbackContext)
    {
        if (_isAiming)
        {
            _lookInput = callbackContext.ReadValue<Vector2>();

            Vector3 lookDirection = new Vector3(_lookInput.x, Vector3.zero.y, _lookInput.y);
            lookDirection.z = 0;
            lookDirection.Normalize();



            transform.rotation = Quaternion.LookRotation(lookDirection);
        }
    }

    private void OnMove()
    {
        if (_movementInput.magnitude > 0.1f)
        {            
            Vector3 direction = _playerCam.transform.forward * _movementInput.y + _playerCam.transform.right * _movementInput.x;
            direction.y = 0;
            direction.Normalize();

            Vector3 movement = direction * _movementSpeed * Time.deltaTime;
            _characterController.Move(movement);
            
            if (!_isAiming)
                RotatePlayer();
        }
    }   

    private void AimingMovement(bool aiming)
    {
        _isAiming = aiming;

        if (aiming)
        {
            InstantRotatePlayer();
            _movementSpeed = _movementDefaultSpeed / 2;
        }
        else
            _movementSpeed = _movementDefaultSpeed;

        _freeLookCamera.SetActive(!aiming);
        _aimCamera.SetActive(aiming);
    }

    private void RotatePlayer()
    {
        Vector3 lookRotation = _playerCam.transform.forward;
        lookRotation.y = 0;
        lookRotation.Normalize();

        Quaternion targetRotation = Quaternion.LookRotation(lookRotation);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
    }

    private void InstantRotatePlayer()
    {
        Vector3 lookRotation = _playerCam.transform.forward;
        lookRotation.y = 0;
        lookRotation.Normalize();
               

        transform.rotation = Quaternion.LookRotation(lookRotation);
    }
}
