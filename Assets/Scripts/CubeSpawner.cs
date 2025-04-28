using UnityEngine;
using Random = UnityEngine.Random;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField, Min(0)] private int _minInstances;
    [SerializeField, Min(0)] private int _maxInstances;
    [SerializeField, Min(0)] private float _spawnRadius;
    [SerializeField, Min(0)] private float _probabilityMultiplier = 0.5f;
    [SerializeField, Min(0)] private float _forceMultiplier = 1.5f;
    [SerializeField, Min(0)] private float _rangeMultiplier = 1.5f;

    private ColorChanger _colorChanger;
    private ScaleChanger _scaleChanger;

    private void OnValidate()
    {
        if (_minInstances > _maxInstances)
            _minInstances = _maxInstances;
    }

    private void Awake()
    {
        _colorChanger = new();
        _scaleChanger = new();
    }

    public void SpawnNextGeneration(Cube original)
    {
        int count = Random.Range(_minInstances, _maxInstances + 1);

        for (int i = 0; i < count; i++)
        {
            Cube cube = Instantiate(original);

            cube.transform.position += new Vector3(RandomizeRadius(), 0, RandomizeRadius());
            cube.name = original.name;

            cube.Init(
                original.ReproductionProbability * _probabilityMultiplier,
                cube.ExplosionForce * _forceMultiplier,
                cube.ExplosionRadius * _rangeMultiplier
            );
            _colorChanger.SetRandomColor(cube.Material);
            _scaleChanger.ChangeScale(cube.transform);
        }
    }

    private float RandomizeRadius()
    {
        return Random.Range(-_spawnRadius, _spawnRadius);
    }
}