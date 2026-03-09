using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyShape")]
public class EnemyShapeData : ScriptableObject
{
    public EnemyUnit prefab;
    public int health = 3;
    public int exp = 1;

    [Header("Grid Size")]
    public int width = 1; // how wide it occupies on x axis (in grid units)
    public int height = 1;

    [Header("Spawning")]
    public int weight = 10;          // relative spawn chance
    public int minDifficulty = 0;
}
