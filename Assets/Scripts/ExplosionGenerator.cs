using UnityEngine;

public class ExplosionGenerator : MonoBehaviour
{
    [SerializeField, Min(1f)] private float _explosionForce;

    public void Explode(Vector3 position, float radius)
    {
        Collider[] colliders = Physics.OverlapSphere(position, radius);

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent<Cube>(out Cube cube))
            {
                Rigidbody cubeRigidbody = collider.GetComponent<Rigidbody>();
                cubeRigidbody.AddExplosionForce(_explosionForce, position, radius);
            }
        }
    }
}
