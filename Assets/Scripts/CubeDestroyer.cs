using System;
using UnityEngine;

public class CubeDestroyer : MonoBehaviour
{
    public event Predicate<Cube> CubeDestroyed;
    [SerializeField] InputSystem _inputSystem;

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
        CubeDestroyed?.Invoke(cube);
    }
}
