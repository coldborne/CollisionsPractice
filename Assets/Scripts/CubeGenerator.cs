using System;
using UnityEngine;

public class CubeGenerator : MonoBehaviour
{
    public Cube Clone(Cube prefab, int splitChance, Vector3 scale)
    {
        Cube cube = Instantiate(prefab);

        cube.Initialize(splitChance, scale);

        cube.gameObject.SetActive(false);

        return cube;
    }
}
