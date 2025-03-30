using System;
using UnityEngine;

public class CubeGenerator : MonoBehaviour
{
    public Cube Clone(Cube prefab)
    {
        Cube cube = Instantiate(prefab);
        cube.gameObject.SetActive(false);

        return cube;
    }

    public Cube[] Clone(Cube prefab, int count)
    {
        if (count <= 0)
        {
            throw new ArgumentException("Количество кубов не может быть отрицательным");
        }

        Cube[] cubes = new Cube[count];

        for (int index = 0; index < cubes.Length; index++)
        {
            cubes[index] = Clone(prefab);
        }

        return cubes;
    }
}
