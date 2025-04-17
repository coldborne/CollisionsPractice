using UnityEngine;

public class SplitRandomizer : MonoBehaviour
{
    private readonly int _minNewCubeCount = 2;
    private readonly int _maxNewCubeCount = 6;

    public bool DoSplit(Cube cube)
    {
        int splitResult = Random.Range(cube.MinSplitChance, cube.MaxSplitChance);

        return splitResult < cube.SplitChance;
    }

    public int GetCubeCount()
    {
        return Random.Range(_minNewCubeCount, _maxNewCubeCount + 1);
    }
}
