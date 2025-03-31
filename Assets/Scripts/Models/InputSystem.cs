using Assets.Scripts.Models;
using System;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    public event Action<Cube> Clicked;

    [SerializeField] private Camera _camera;

    private void Awake()
    {
        if (_camera == null)
        {
            _camera = Camera.main;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown((int)MouseButtonClickValue.Left))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform.TryGetComponent<Cube>(out Cube cube))
                {
                    Clicked?.Invoke(cube);
                }
            }
        }
    }
}