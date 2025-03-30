using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(MeshRenderer))]
public class Cube : MonoBehaviour
{
    [SerializeField] private Color[] _colors;

    private Rigidbody _rigidbody;
    private BoxCollider _boxCollider;
    private MeshRenderer _meshRenderer;

    public int SeparationChance { get; private set; }

    public int MinSeparationChance => 0;
    public int MaxSeparationChance => 100;

    private void Awake()
    {
        if (MinSeparationChance > MaxSeparationChance)
        {
            throw new Exception("Минимальный шанс не может быть больше максимального");
        }

        _rigidbody = GetComponent<Rigidbody>();

        if (_rigidbody.useGravity == false)
        {
            throw new UnityException($"При создании объекта класса: {this.GetType()}. В компоненте {_rigidbody.GetType()} гравитация установлена в значение false");
        }

        _boxCollider = GetComponent<BoxCollider>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public void Initialize(int decompositionChance, Vector3 scale)
    {
        if (decompositionChance < MinSeparationChance || decompositionChance > MaxSeparationChance)
        {
            throw new ArgumentOutOfRangeException("Шанс разделения вне допустимых пределах");
        }

        int minColorIndex = 0;
        int maxColorIndex = _colors.Length - 1;
        int colorIndex = UnityEngine.Random.Range(minColorIndex, maxColorIndex + 1);

        Color color = _colors[colorIndex];

        SeparationChance = decompositionChance;
        transform.localScale = scale;

        _meshRenderer.material.color = color;
    }
}
