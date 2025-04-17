using System.Collections.Generic;
using UnityEngine;

public class ExplosionGenerator : MonoBehaviour
{
    [SerializeField, Min(1f)] private float _explosionForce;

    public void Explode(IEnumerable<Rigidbody> cubes, Vector3 position, float radius)
    {
        foreach (Rigidbody cube in cubes)
        {
            cube.AddExplosionForce(_explosionForce, position, radius);
        }
    }
}
