using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody _rb;

    private float _bulletLifetime = 5f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        Destroy(gameObject, _bulletLifetime);
    }    

    private void OnCollisionEnter(Collision collision)
    {              
        Destroy(gameObject);
    }
    
    public void BulletInitialize(float firePower, Vector3 direction)
    {
        _rb.linearVelocity = direction.normalized * firePower;        
    }    
}
