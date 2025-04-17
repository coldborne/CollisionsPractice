using UnityEngine;

public class CubeDestroyer : MonoBehaviour
{
    public void Destroy(Cube cube)
    {
        Destroy(cube.gameObject);
    }
}
