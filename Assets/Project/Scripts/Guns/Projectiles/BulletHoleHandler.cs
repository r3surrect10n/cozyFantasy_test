using UnityEngine;

public class BulletHoleHandler : MonoBehaviour
{
    [Header("Bullet impact settings")]
    [SerializeField] private GameObject _holeDecal;
    [SerializeField, Range(0, 15)] private float _decalLifetime = 10f;
    [SerializeField, Range(0, 1)] private float _decalSize = 0.3f;

    public void CreateBulletHole(Vector3 hittedPoint, Vector3 normal, Transform collision)
    {
        Vector3 decalPosition = hittedPoint;
        Quaternion decalRotation = Quaternion.LookRotation(normal);

        GameObject decal = Instantiate(_holeDecal, decalPosition, decalRotation);

        decal.transform.localScale = Vector3.one * _decalSize;
        decal.transform.parent = collision.transform;

        Destroy(decal, _decalLifetime);
    }
}
