using UnityEngine;

public class Grenade : MonoBehaviour
{
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void GrenadeInitialize(float throwPower, Vector3 direction)
    {
        Debug.Log(direction);
        _rb.AddForce(direction * throwPower, ForceMode.Impulse);
    }
}
