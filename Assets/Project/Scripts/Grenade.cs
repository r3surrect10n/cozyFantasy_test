using System.Collections;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
[RequireComponent (typeof(MeshRenderer))]
public class Grenade : MonoBehaviour
{
    [Header("Grenade settings")]
    [SerializeField] private GameObject _particles;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField, Range (0, 10)] private float _greandeLifetime;

    private Rigidbody _rb;
    private MeshRenderer _mesh;    

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _mesh = GetComponent<MeshRenderer>();        
    }

    public void GrenadeInitialize(float throwPower, Vector3 direction)
    {        
        _rb.AddForce(direction * throwPower, ForceMode.Impulse);

        StartCoroutine(GrenadeExplosion());
    }  

    private IEnumerator GrenadeExplosion()
    {
        while (_greandeLifetime > 0)
        {
            _greandeLifetime -= Time.deltaTime;
            yield return null;
        }

        _mesh.enabled = false;
        _rb.isKinematic = true;
        
        _particles.SetActive(true);
        _particles.transform.localRotation = Quaternion.identity;
        Destroy(gameObject, _particleSystem.main.duration);
    }
}
