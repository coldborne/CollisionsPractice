using Assets.Scripts.Models;
using System;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    public event Action<Vector3> Clicked;

    private void Update()
    {
        if (Input.GetMouseButtonDown((int)MouseButtonClickValue.Left))
        {
            Clicked?.Invoke(Input.mousePosition);
        }
    }
}