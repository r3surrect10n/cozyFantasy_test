using UnityEngine;

public class Pistol : MonoBehaviour, IShootable
{
    [Header("Gun settings")]
    [SerializeField] private Transform _pistolShootPoint;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private ParticleSystem _muzzleParticles;    
    [SerializeField, Range(0, 300)] private float _bulletPower = 150f;
    [SerializeField, Range (50, 300)] private float _gunPower;

    [Header("Bullet impact settings")]
    [SerializeField] private GameObject _holeDecal;
    [SerializeField, Range(0, 15)] private float _decalLifetime = 10f;
    [SerializeField, Range(0, 1)] private float _decalSize = 0.1f;

    private Vector3 _targetPoint;

    public void Shoot(RaycastHandler raycastHandler)
    {
        _muzzleParticles.Play();

        RaycastHit shootHit = raycastHandler.ShooterHit;

        if (shootHit.collider != null)
            _targetPoint = shootHit.point;
        else
            _targetPoint = raycastHandler.CameraTransform.position + raycastHandler.CameraTransform.forward * _gunPower;

        Vector3 shootDirection = (_targetPoint - _pistolShootPoint.position).normalized;

        Bullet bullet = Instantiate(_bullet, _pistolShootPoint.position, Quaternion.LookRotation(shootDirection));
        bullet.BulletInitialize(_bulletPower, shootDirection, shootHit);              
    }

    public void StopShooting()
    {
        return;
    }   
}
