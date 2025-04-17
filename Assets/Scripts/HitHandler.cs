using System;
using System.Collections.Generic;
using UnityEngine;

public class HitHandler : MonoBehaviour
{
    [SerializeField] private InputSystem _inputSystem;
    [SerializeField] private Camera _camera;

    [SerializeField] private SplitRandomizer _splitRandomizer;
    [SerializeField] private CubeDestroyer _cubeDestroyer;
    [SerializeField] private ExplosionGenerator _explosionGenerator;

    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private CubeGenerator _cubeGenerator;

    [SerializeField, Min(0.1f)] private float _spawnRadius;

    private void Awake()
    {
        if (_inputSystem == null)
        {
            throw new NullReferenceException($"Обработчику попаданий не установлено поле: {typeof(InputSystem)}");
        }

        if (_camera == null)
        {
            _camera = Camera.main;
        }
    }

    private void OnEnable()
    {
        _inputSystem.Clicked += Handle;
    }

    private void OnDisable()
    {
        _inputSystem.Clicked -= Handle;
    }

    private void Handle(Vector3 mousePosition)
    {
        Ray ray = _camera.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform.TryGetComponent(out Cube destroyableCube))
            {
                bool doSplit = _splitRandomizer.DoSplit(destroyableCube);

                if (doSplit)
                {
                    int newCubeCount = _splitRandomizer.GetCubeCount();
                    List<Rigidbody> rigidbodies = new List<Rigidbody>();

                    int splitChanceModifier = 2;
                    int scaleModifier = 2;

                    int newCubeSplitChance = destroyableCube.SplitChance / splitChanceModifier;
                    Vector3 newCubeScale = destroyableCube.transform.localScale / scaleModifier;

                    for (int cubeNumber = 1; cubeNumber <= newCubeCount; cubeNumber++)
                    {
                        Cube cube = _cubeGenerator.Clone(destroyableCube, newCubeSplitChance, newCubeScale);

                        _cubeSpawner.Spawn(cube, _spawnRadius, Quaternion.identity);

                        if (cube.TryGetComponent(out Rigidbody rigidbody))
                        {
                            rigidbodies.Add(rigidbody);
                        }
                    }

                    _explosionGenerator.Explode(rigidbodies, destroyableCube.transform.position, _spawnRadius);
                }

                _cubeDestroyer.Destroy(destroyableCube);
            }
        }
    }
}
