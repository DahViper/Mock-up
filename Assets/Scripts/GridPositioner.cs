using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct RowLayoutResult
{
    public List<(EnemyShapeData data, int slotX)> shapes;
    public int maxHeight;
}
public static class RowLayoutSolver
{
    public static RowLayoutResult SolveRow(
        EnemyShapeData[] shapes,
        int gridWidth,
        int difficulty,
        float fillChance
    )
    {
        bool[] occupied = new bool[gridWidth];
        var result = new List<(EnemyShapeData, int)>();
        int maxHeight = 1;

        for (int x = 0; x < gridWidth; x++)
        {
            if (occupied[x]) continue;
            if (Random.value > fillChance) continue;

            var candidates = GetFittingShapes(shapes, occupied, x, gridWidth, difficulty);
            if (candidates.Count == 0) continue;

            var shape = WeightedPicker.Pick(candidates);

            for (int i = 0; i < shape.width; i++)
                occupied[x + i] = true;

            result.Add((shape, x));
            maxHeight = Mathf.Max(maxHeight, shape.height);
        }

        return new RowLayoutResult
        {
            shapes = result,
            maxHeight = maxHeight
        };
    }

    static List<EnemyShapeData> GetFittingShapes(
        EnemyShapeData[] shapes,
        bool[] occupied,
        int startX,
        int gridWidth,
        int difficulty
    )
    {
        var list = new List<EnemyShapeData>();

        foreach (var s in shapes)
        {
            if (s.minDifficulty > difficulty) continue;
            if (startX + s.width > gridWidth) continue;

            bool fits = true;
            for (int i = 0; i < s.width; i++)
            {
                if (occupied[startX + i])
                {
                    fits = false;
                    break;
                }
            }

            if (fits)
                list.Add(s);
        }

        return list;
    }
}


