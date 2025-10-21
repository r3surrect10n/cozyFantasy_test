using UnityEngine;

[RequireComponent (typeof(Gun))]
public class Pistol : MonoBehaviour, IShootable
{
    [Header("Gun settings")]
    [SerializeField] private Transform _pistolShootPoint;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private ParticleSystem _muzzleParticles;    
    [SerializeField, Range(0, 300)] private float _bulletPower = 150f;
    [SerializeField, Range(0, 50)] private float _bulletDamage = 15f;
    [SerializeField, Range (50, 300)] private float _gunPower;    

    private Gun _gun;

    private void Awake()
    {
        _gun = GetComponent<Gun>();
    }

    public void Shoot(RaycastHandler raycastHandler)
    {
        _gun.GunShot(_muzzleParticles, raycastHandler, _pistolShootPoint, _bullet, _gunPower, _bulletPower, _bulletDamage);
    }

    public void StopShooting()
    {
        return;
    }   
}
