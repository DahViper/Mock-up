using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyController : MonoBehaviour
{
    public int currentDifficulty { get; private set; }
    public int rowsPerLevel = 5;

    int rowsSpawned;

    public void OnRowSpawned()
    {
        rowsSpawned++;

        if (rowsSpawned % rowsPerLevel == 0)
            currentDifficulty++;
    }

    public float GetFillChance()
    {
        // early game sparse, late game dense
        return Mathf.Clamp01(0.4f + currentDifficulty * 0.08f);
    }
}

public static class WeightedPicker
{
    public static EnemyShapeData Pick(List<EnemyShapeData> shapes)
    {
        int totalWeight = 0;
        foreach (var s in shapes)
            totalWeight += s.weight;

        int roll = Random.Range(0, totalWeight);
        int cumulative = 0;

        foreach (var s in shapes)
        {
            cumulative += s.weight;
            if (roll < cumulative)
                return s;
        }

        return shapes[0];
    }
}
