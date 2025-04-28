using UnityEngine;

[RequireComponent(typeof(CubeSpawner))]
[RequireComponent(typeof(CubeExploder))]
public class CubeRaycaster : MonoBehaviour
{
    private CubeSpawner _cubeSpawner;
    private CubeExploder _cubeExploder;

    private readonly float _maxDistance = 100;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
        _cubeSpawner = GetComponent<CubeSpawner>();
        _cubeExploder = GetComponent<CubeExploder>();
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
            _cubeExploder.Explode(origin);

            return;
        }

        _cubeSpawner.SpawnNextGeneration(origin);
    }
}