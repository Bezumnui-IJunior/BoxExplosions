using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshRenderer))]
public class Cube : MonoBehaviour
{
    [SerializeField, Min(0)] private float _reproductionProbability = 1f;
    [SerializeField, Min(0)] private float _explosionRadius = 1f;
    [SerializeField, Min(0)] private float _explosionForce = 1f;
    [SerializeField, Min(0)] private float _explosionUpFactor = 1f;

    private MeshRenderer _meshRenderer;
    private bool _isInit;

    public float ReproductionProbability => _reproductionProbability;
    public float ExplosionRadius => _explosionRadius;
    public float ExplosionForce => _explosionForce;
    public float ExplosionUpFactor => _explosionUpFactor;
    public Rigidbody Rigidbody { get; private set; }
    public Material Material => _meshRenderer.material;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public bool ShouldReproduce() => Random.value < _reproductionProbability;

    public void Init(float reproductionProbability, float explosionForce, float explosionRadius)
    {
        if (_isInit)
            return;

        _reproductionProbability = reproductionProbability;
        _explosionForce = explosionForce;
        _explosionRadius = explosionRadius;
        _isInit = true;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}