using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spawn/ShapePattern")]
public class ShapePattern : ScriptableObject
{
    public int width = 5;
    public int height = 3;

    public float cellSize = 0.9f;

    [Tooltip("1 = occupied, 0 = empty")]
    public int[] cells; // flattened: row-major

    public bool IsFilled(int x, int y)
    {
        return cells[y * width + x] == 1;
    }
}
