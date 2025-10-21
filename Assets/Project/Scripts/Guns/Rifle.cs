using System.Collections;
using UnityEngine;

[RequireComponent (typeof(Gun))]
public class Rifle : MonoBehaviour, IShootable
{
    [Header("Gun settings")]
    [SerializeField] private Transform _rifleShootPoint;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private ParticleSystem _muzzleParticles;
    [SerializeField, Range(0, 1)] private float _rifleShootCD;
    [SerializeField, Range(0, 300)] private float _bulletPower = 150f;
    [SerializeField, Range(0, 50)] private float _bulletDamage = 25;
    [SerializeField, Range(50, 300)] private float _gunPower;

    private Gun _gun;    

    private void Awake()
    {
        _gun = GetComponent<Gun>();
    }

    public void Shoot(RaycastHandler raycastHandler)
    {
        StartCoroutine(Shooting(raycastHandler));
    }

    public void StopShooting()
    {
        StopAllCoroutines();
    }

    private IEnumerator Shooting(RaycastHandler raycastHandler)
    {
        while (true)
        {
            _gun.GunShot(_muzzleParticles, raycastHandler, _rifleShootPoint, _bullet, _gunPower, _bulletPower, _bulletDamage);
            yield return new WaitForSeconds(_rifleShootCD);
        }
    }    
}
