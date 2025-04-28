using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CubeSpawner))]
public class CubeRaycaster : MonoBehaviour
{
    private readonly float _maxDistance = 100;

    private CubeSpawner _cubeSpawner;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
        _cubeSpawner = GetComponent<CubeSpawner>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) == false)
            return;

        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, _maxDistance) == false)
            return;

        if (hit.transform.gameObject.TryGetComponent(out Cube origin) == false)
            return;

        if (origin.ShouldReproduce() == false)
        {
            origin.Destroy();

            return;
        }

        List<Cube> cubes = _cubeSpawner.SpawnNextGeneration(origin);
        StartCoroutine(Explode(origin, cubes));
    }

    private IEnumerator Explode(Cube origin, List<Cube> cubes)
    {
        yield return new WaitForFixedUpdate();

        foreach (Cube cube in cubes)
        {
            cube.Rigidbody.AddExplosionForce(
                origin.ExplosionForce,
                origin.transform.position,
                origin.ExplosionRadius,
                0,
                ForceMode.Impulse
            );
        }

        origin.Destroy();
    }
}