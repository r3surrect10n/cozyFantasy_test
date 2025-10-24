using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Health))]
public class DestructableObject : MonoBehaviour
{
    public event Action OnDestroy;

    private Health _health;    

    private void Awake()
    {
        _health = GetComponent<Health>();               
    }

    private void OnEnable()
    {
        _health.OnDestruct += DestroyObject;
    }

    private void OnDisable()
    {
        _health.OnDestruct -= DestroyObject;
    }

    private void OnValidate()
    {
        if (_health == null) 
            _health = GetComponent<Health>();
    }

    private void DestroyObject()
    {
        OnDestroy?.Invoke();
        Destroy(gameObject);
    }    
}
