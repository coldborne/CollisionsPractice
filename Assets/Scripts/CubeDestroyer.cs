using System;
using UnityEngine;

public class CubeDestroyer : MonoBehaviour
{
    [SerializeField] private HitHandler _hitHandler;

    public event Predicate<Cube> Destroyed;

    private void OnEnable()
    {
        _hitHandler.Hit += Destroy;
    }

    private void OnDisable()
    {
        _hitHandler.Hit -= Destroy;
    }

    private void Destroy(Cube cube)
    {
        Destroy(cube.gameObject);
        Destroyed?.Invoke(cube);
    }
}
