using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeightedShape
{
    public ShapePattern shape;
    public float baseWeight = 10f;
    public float difficultyMultiplier = 1f;
}
