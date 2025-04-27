using UnityEngine;
using Random = UnityEngine.Random;

public class ScaleChanger
{
    private readonly float _scaleMultiplier = 0.5f;

    public void ChangeScale(Transform transform)
    {
        transform.localScale *= _scaleMultiplier;
    }
}