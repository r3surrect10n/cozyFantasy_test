using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private ItemType _gunType;

    public ItemType GunType => _gunType;

    private PlayerInventory _inventory;

    private Vector3 _targetPoint;

    private void Awake()
    {
        _inventory = GetComponentInParent<PlayerInventory>();
    }

    public void GunShot(ParticleSystem particles, RaycastHandler raycastHandler, Transform shootPoint, Bullet bulletType, float gunPower, float bulletPower, float bulletDamage)
    {
        if (_inventory.HaveItem(ItemType.Ammo, out int count) && count > 0)
        {
            _inventory.RemoveInventoryItem(ItemType.Ammo, 1);

            particles.Play();

            RaycastHit shootHit = raycastHandler.ShooterHit;

            if (shootHit.collider != null)
                _targetPoint = shootHit.point;
            else
                _targetPoint = raycastHandler.CameraTransform.position + raycastHandler.CameraTransform.forward * gunPower;

            Vector3 shootDirection = (_targetPoint - shootPoint.position).normalized;

            Bullet bullet = Instantiate(bulletType, shootPoint.position, Quaternion.LookRotation(-shootDirection));
            bullet.BulletInitialize(bulletPower, shootDirection, bulletDamage);
        }
    }
}
