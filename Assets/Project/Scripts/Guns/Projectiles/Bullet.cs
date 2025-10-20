using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet impact settings")]
    [SerializeField] private GameObject _holeDecal;
    [SerializeField, Range(0, 15)] private float _decalLifetime = 10f;
    [SerializeField, Range(0, 1)] private float _decalSize = 0.3f;

    private Rigidbody _rb;
    private RaycastHit _bulletHit;

    private float _bulletLifetime = 5f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        Destroy(gameObject, _bulletLifetime);
    }    

    private void OnCollisionEnter(Collision collision)
    {
        _rb.isKinematic = true;

        ContactPoint contact = collision.contacts[0];
        Vector3 decalPosition = contact.point;
        Quaternion decalRotation = Quaternion.LookRotation(contact.normal);
        
        GameObject decal = Instantiate(_holeDecal, decalPosition, decalRotation);

        decal.transform.localScale = Vector3.one * _decalSize;
        decal.transform.parent = collision.transform;

        Destroy(decal, _decalLifetime);

        Destroy(gameObject);
    }
    
    public void BulletInitialize(float firePower, Vector3 direction, RaycastHit hit)
    {
        _bulletHit = hit;
        _rb.linearVelocity = direction.normalized * firePower;        
    }    
}
