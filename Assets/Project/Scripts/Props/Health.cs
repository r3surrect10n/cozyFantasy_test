using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamagable
{    
    public event Action OnDestruct;

    [SerializeField] private float _health;

    public void ApplyDamage(float damageAmount)
    {
        _health -= damageAmount;

        if (_health <= 0)
            OnDestruct?.Invoke();
    }
}
