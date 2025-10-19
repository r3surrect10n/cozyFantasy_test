using UnityEngine;

public class DestructableObject : MonoBehaviour, IDestructable
{
    public void Destroy()
    {
        Destroy(gameObject);
    }    
}
