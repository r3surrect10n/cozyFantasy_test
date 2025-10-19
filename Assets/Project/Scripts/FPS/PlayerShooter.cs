using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent (typeof(RaycastHandler))]
public class PlayerShooter : MonoBehaviour
{
    public event Action<bool> OnAim;

    [Header("Player camera")]
    [SerializeField] private Camera _playerCam;

    [Header("Shoot settings")]
    [SerializeField] private Transform _rifleShootPoint;
    [SerializeField, Range(0, 200)] private float _shootDistance;
    [SerializeField, Range(0, 1)] private float _rifleShootCD;
    [SerializeField, Range(0, 300)] private float _bulletPower = 150f;
    [SerializeField] private ParticleSystem _muzzleParticles;

    [Header("Grenade throw settings")]
    [SerializeField] private Grenade _grenade;
    [SerializeField] private Transform _throwPoint;
    [SerializeField, Range (0, 60)] private float _throwAngle = 15f;
    [SerializeField, Range (0, 20)] private float _throwPower = 5f;

    [Header("Bullet impact settings")]
    [SerializeField] private Bullet _bullet;
    [SerializeField] private GameObject _holeDecal;
    [SerializeField, Range (0, 15)] private float _decalLifetime = 10f;
    [SerializeField, Range(0, 1)] private float _decalSize = 0.1f;


    private RaycastHandler _raycastHandler;

    private Ray _screenCenter;
    private RaycastHit _shootHit;

    private Coroutine _shooterCoroutine;

    private Vector3 _cameraForward;

    private bool _isAiming = false;

    private void Update()
    {
        PlayerLook();
    }

    private void OnValidate()
    {
        if (_raycastHandler == null)
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
        if (callbackContext.started && _shooterCoroutine == null)        
            _shooterCoroutine = StartCoroutine(Shooting());
        else if (callbackContext.canceled && _shooterCoroutine != null)
        {
            StopCoroutine(_shooterCoroutine);
            _shooterCoroutine = null;
        }
    }

    public void Grenade(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.started && !_isAiming)
        {
            Grenade newGrenade = Instantiate(_grenade, _throwPoint.position, Quaternion.identity);
            Quaternion upperAngle = Quaternion.Euler(-_throwAngle, Vector3.zero.y, Vector3.zero.z);            
            newGrenade.GrenadeInitialize(_throwPower, upperAngle * _cameraForward);
        }
    }

    private void AimState()
    {
        _isAiming = !_isAiming;
        OnAim?.Invoke(_isAiming);
    }

    private void PlayerLook()
    {
        _screenCenter = _playerCam.ScreenPointToRay(new Vector3( Screen.width / 2, Screen.height / 2, 0));
        Debug.DrawRay(_screenCenter.origin, _screenCenter.direction * _shootDistance, Color.blue);

        _cameraForward = _playerCam.transform.forward;
    }

    private IEnumerator Shooting()
    {
        while (true)
        {
            _muzzleParticles.Play();

            Bullet bullet = Instantiate(_bullet, _rifleShootPoint.position, Quaternion.LookRotation(-_cameraForward));
            bullet.BulletInitialize(_bulletPower, _cameraForward);

            if (Physics.Raycast(_screenCenter, out _shootHit, _shootDistance))
            {
                if (_shootHit.collider != null)
                {
                    CreateBulletHole(_shootHit);
                }

                if (_shootHit.collider.TryGetComponent<IDestructable>(out var destructable))
                    destructable.Destroy();
                else
                    yield return null;
            }

            yield return new WaitForSeconds(_rifleShootCD);
        }
    }

    private void CreateBulletHole(RaycastHit hit)
    { 
        Vector3 decalPosition = hit.point;
        Quaternion decalRotation = Quaternion.LookRotation(hit.normal);
        GameObject decal = Instantiate(_holeDecal, decalPosition, decalRotation);

        decal.transform.localScale = Vector3.one * _decalSize;
        decal.transform.parent = hit.transform;

        Destroy(decal, _decalLifetime);
    }
}
