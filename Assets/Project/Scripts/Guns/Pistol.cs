using UnityEngine;

public class Pistol : MonoBehaviour, IShootable
{
    [Header("Gun settings")]
    [SerializeField] private Transform _pistolShootPoint;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private ParticleSystem _muzzleParticles;    
    [SerializeField, Range(0, 300)] private float _bulletPower = 150f;

    [Header("Bullet impact settings")]
    [SerializeField] private GameObject _holeDecal;
    [SerializeField, Range(0, 15)] private float _decalLifetime = 10f;
    [SerializeField, Range(0, 1)] private float _decalSize = 0.1f;

    public void Shoot(RaycastHandler raycastHandler)
    {
        _muzzleParticles.Play();

        Bullet bullet = Instantiate(_bullet, _pistolShootPoint.position, Quaternion.LookRotation(-raycastHandler.CameraTransform.forward));
        bullet.BulletInitialize(_bulletPower, raycastHandler.CameraTransform.forward);

        RaycastHit shootHit = raycastHandler.ShooterHit;

        if (shootHit.collider != null)
        {
            //CreateBulletHole(shootHit);

            if (shootHit.collider.TryGetComponent<IDestructable>(out var destructable))
                destructable.Destroy();
            else
                return;
        }
    }

    public void StopShooting()
    {
        return;
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
