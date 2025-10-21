using UnityEngine;

public class Bullet : MonoBehaviour
{  
    private Rigidbody _rb;
    private BulletHoleHandler _bulletHole;

    private float _bulletLifetime = 5f;
    private float _damage;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _bulletHole = GetComponent<BulletHoleHandler>();
        Destroy(gameObject, _bulletLifetime);
    }    

    private void OnCollisionEnter(Collision collision)
    {
        _rb.isKinematic = true;

        ContactPoint contact = collision.contacts[0];
        _bulletHole.CreateBulletHole(contact.point, contact.normal, collision.transform);

        if (collision.gameObject.TryGetComponent<IDamagable>(out var damagable))
            damagable.ApplyDamage(_damage);

        Destroy(gameObject);
    }
    
    public void BulletInitialize(float firePower, Vector3 direction, float bulletDamage)
    {        
        _rb.linearVelocity = direction.normalized * firePower;        
        _damage = bulletDamage;
    }    
}
