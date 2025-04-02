using UnityEngine;

public class ExplosionGenerator : MonoBehaviour
{
    [SerializeField, Min(1f)] private float _explosionForce;

    public void Explode(Cube[] cubes, Vector3 position, float radius)
    {
        foreach (Cube cube in cubes)
        {
            cube.AddExplosionForce(_explosionForce, position, radius);
        }
    }
}
