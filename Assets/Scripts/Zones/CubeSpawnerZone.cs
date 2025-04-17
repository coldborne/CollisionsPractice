using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
public class CubeSpawnerZone : MonoBehaviour
{
    [SerializeField] private CubeGenerator _cubeGenerator;
    [SerializeField] private CubeSpawner _cubeSpawner;

    [SerializeField] private Cube[] _cubePrefabs;
    [SerializeField] private bool _hasStoped = false;

    [SerializeField] private float _delay;

    private MeshCollider _collider;
    private Bounds _spawnZoneBounds;

    private Coroutine _spawnCubeCoroutine;
    private WaitForSecondsRealtime _waitForSeconds;
    private bool _isSpawning;

    private void Awake()
    {
        _collider = GetComponent<MeshCollider>();
        _spawnZoneBounds = _collider.bounds;
        _waitForSeconds = new WaitForSecondsRealtime(_delay);
        _isSpawning = true;

        _spawnCubeCoroutine = StartCoroutine(SpawnNext());
    }

    [ContextMenu("Включить/Выключить создание кубов")]
    private void Toggle()
    {
        if (_isSpawning)
        {
            StopCoroutine(_spawnCubeCoroutine);
        }
        else
        {
            _spawnCubeCoroutine = StartCoroutine(SpawnNext());
        }
    }

    private IEnumerator SpawnNext()
    {
        while (_hasStoped == false)
        {
            Spawn();

            yield return _waitForSeconds;
        }
    }

    private void Spawn()
    {
        Vector3 position = new Vector3(
            Random.Range(_spawnZoneBounds.min.x, _spawnZoneBounds.max.x),
            _spawnZoneBounds.min.y,
            Random.Range(_spawnZoneBounds.min.z, _spawnZoneBounds.max.z));

        int cubePrefabIndex = Random.Range(0, _cubePrefabs.Length);
        Cube prefab = _cubePrefabs[cubePrefabIndex];
        int startPrefabSplitChance = 100;
        Vector3 startPrefabScale = new Vector3(1, 1, 1);

        Cube cube = _cubeGenerator.Clone(prefab, startPrefabSplitChance, startPrefabScale);

        _cubeSpawner.Spawn(cube, position, Quaternion.identity);
    }
}
