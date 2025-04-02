using System;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private CubeDestroyer _cubeDestroyer;
    [SerializeField] private CubeGenerator _cubeGenerator;
    [SerializeField] private ExplosionGenerator _explosionGenerator;

    [SerializeField, Min(0.1f)] private float _spawnRadius;

    private readonly int _minNewCubeCount = 2;
    private readonly int _maxNewCubeCount = 6;

    private void Awake()
    {
        if (_cubeDestroyer == null)
        {
            throw new NullReferenceException($"Создателю кубов не установлено поле: {typeof(CubeDestroyer)}");
        }

        if (_cubeGenerator == null)
        {
            throw new NullReferenceException($"Создателю кубов не установлено поле: {typeof(CubeGenerator)}");
        }

        if (_explosionGenerator == null)
        {
            throw new NullReferenceException($"Создателю кубов не установлено поле: {typeof(ExplosionGenerator)}");
        }
    }

    private void OnEnable()
    {
        _cubeDestroyer.Destroyed += TrySpawn;
    }

    private void OnDisable()
    {
        _cubeDestroyer.Destroyed -= TrySpawn;
    }

    private bool TrySpawn(Cube destroyableCube)
    {
        int separationResult = UnityEngine.Random.Range(destroyableCube.MinSeparationChance, destroyableCube.MaxSeparationChance);

        if (separationResult <= destroyableCube.SeparationChance)
        {
            int separationChanceModifier = 2;
            int scaleModifier = 2;

            int newCubeCount = UnityEngine.Random.Range(_minNewCubeCount, _maxNewCubeCount + 1);
            Cube[] cubes = _cubeGenerator.Clone(destroyableCube, newCubeCount);

            foreach (Cube cube in cubes)
            {
                Vector3 cubePosition = destroyableCube.transform.position + UnityEngine.Random.insideUnitSphere * _spawnRadius;
                cube.transform.position = cubePosition;

                int newCubeSeparationChance = destroyableCube.SeparationChance / separationChanceModifier;
                Vector3 newCubeScale = destroyableCube.transform.localScale / scaleModifier;

                cube.Initialize(newCubeSeparationChance, newCubeScale);
                cube.gameObject.SetActive(true);
            }

            _explosionGenerator.Explode(cubes, destroyableCube.transform.position, _spawnRadius);

            return true;
        }

        return false;
    }
}
