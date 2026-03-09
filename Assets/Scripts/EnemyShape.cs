using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyShape
{
    Single,      // 1x1
    LShape,      
    HorizontalRect, // 1x3 or 1x2
    VerticalRect,   // 3x1 or 2x1
    BigSquare,   // 2x2
    TShape,      
    ZShape       
}

[System.Serializable]
public class ShapeDefinition
{
    public EnemyShape shapeType;
    public Vector2Int[] blockPositions;
    public int health;
    public int pointValue;
    public Color color;

    public ShapeDefinition(EnemyShape type, Vector2Int[] positions, int hp, int points, Color col)
    {
        shapeType = type;
        blockPositions = positions;
        health = hp;
        pointValue = points;
        color = col;
    }
}
