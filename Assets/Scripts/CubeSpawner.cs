using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField, Min(0)] private int _minInstances;
    [SerializeField] private int _maxInstances;
    [SerializeField] private float _range;
    [SerializeField] private float _probabilityMultiplier = 0.5f;
    
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

    public List<Cube> SpawnNextGeneration(Cube original)
    {
        int count = Random.Range(_minInstances, _maxInstances + 1);
        List<Cube> cubes = new();

        for (int i = 0; i < count; i++)
        {
            Cube cube = Instantiate(original);
            cube.transform.position += new Vector3(Random.Range(-_range, _range), 0, Random.Range(-_range, _range));
            cube.name = gameObject.name;
            cube.Init(original.ReproductionProbability * _probabilityMultiplier);
            _colorChanger.SetRandomColor(cube.Material);
            _scaleChanger.ChangeScale(cube.transform);
            cubes.Add(cube);
        }

        return cubes;
    }
}