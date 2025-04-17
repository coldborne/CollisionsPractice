using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public void Spawn(Cube cube, float spawnRadius, Quaternion quaternion)
    {
        cube.transform.position = cube.transform.position + Random.insideUnitSphere * spawnRadius;

        cube.gameObject.SetActive(true);
    }

    public void Spawn(Cube cube, Vector3 position, Quaternion quaternion)
    {
        cube.transform.position = position;

        cube.gameObject.SetActive(true);
    }
}
