using System;
using UnityEngine;

public class HitHandler : MonoBehaviour
{
    [SerializeField] private InputSystem _inputSystem;
    [SerializeField] private Camera _camera;

    public event Action<Cube> Hit;

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
            if (hit.transform.TryGetComponent<Cube>(out Cube cube))
            {
                Hit?.Invoke(cube);
            }
        }
    }
}
