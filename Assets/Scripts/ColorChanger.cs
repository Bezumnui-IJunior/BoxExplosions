using UnityEngine;
using Random = UnityEngine.Random;

public class ColorChanger
{
    public void SetRandomColor(Material material)
    {
        material.color = Random.ColorHSV();
    }
}