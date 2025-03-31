using System;
using UnityEngine;

public class CubeDestroyer : MonoBehaviour
{
    [SerializeField] private InputSystem _inputSystem;

    public event Predicate<Cube> Destroyed;

    private void OnEnable()
    {
        _inputSystem.Clicked += Destroy;
    }

    private void OnDisable()
    {
        _inputSystem.Clicked -= Destroy;
    }

    private void Destroy(Cube cube)
    {
        Destroy(cube.gameObject);
        Destroyed?.Invoke(cube);
    }
}
