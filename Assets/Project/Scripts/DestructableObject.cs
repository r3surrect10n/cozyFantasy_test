using UnityEngine;

public class DestructableObject : MonoBehaviour
{    
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Bullet>() != null)
        {
            
            _meshRenderer.enabled = false;
            Destroy(gameObject);
        }
    }
}
