using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(DestructableObject))]
public class SpawnItems : MonoBehaviour
{
    [Header("Item prefabs")]
    [SerializeField] private List<GameObject> _spawnItems = new List<GameObject>();

    private DestructableObject _object;

    private void Start()
    {
        _object = GetComponent<DestructableObject>();
        _object.OnDestroy += SpawnItem;
    }

    private void OnDisable()
    {
        _object.OnDestroy -= SpawnItem;
    }

    private void SpawnItem()
    {
        int randomItem = Random.Range(0, 3);
        switch (randomItem)
        {
            case 0:
                ItemType(_spawnItems[0]);
                break;
            case 1:
                ItemType(_spawnItems[1]);
                break;
            case 2:
                break;
        }

    }

    private void ItemType(GameObject item)
    {
        Instantiate(item, transform.position, Quaternion.identity);
    }
}
