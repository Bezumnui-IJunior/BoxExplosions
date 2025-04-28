using UnityEngine;

public class CubeExploder : MonoBehaviour
{
    [SerializeField, Min(0)] private int _maxColliders = 100;

    private Collider[] _colliders;

    private void Awake()
    {
        _colliders = new Collider[_maxColliders];
    }

    public void Explode(Cube cube)
    {
        int size = Physics.OverlapSphereNonAlloc(cube.transform.position, cube.ExplosionRadius, _colliders);

        for (int i = 0; i < size; i++)
        {
            Collider hitCollider = _colliders[i];

            if (hitCollider.TryGetComponent(out Rigidbody hitRigidbody) == false)
                continue;

            hitRigidbody.AddExplosionForce(
                cube.ExplosionForce,
                cube.transform.position,
                cube.ExplosionRadius,
                cube.ExplosionUpFactor,
                ForceMode.Impulse
            );
        }
    }
}