using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDirector : MonoBehaviour
{
    public EnemyShapeData[] shapes;

    public float spawnY = 6f;
    public float rowSpacing = 1.5f;
    public float moveSpeed = 1f;

    public int gridWidth = 8;
    public float gridSize = 1.2f;
    public float leftLimit = -6f;
    public float rightLimit = 6f;

    public float leftEdge = -4.8f;

    private float nextSpawnY;

    public DifficultyController difficulty;
    void Start()
    {
        nextSpawnY = spawnY;
        SpawnRow();
    }

    void Update()
    {
        MoveDown();

        if (ShouldSpawnNextRow())
        {
            SpawnRow();
            nextSpawnY = spawnY;
        }
    }

    void MoveDown()
    {
        float delta = moveSpeed * Time.deltaTime;

        for (int i = 0; i < EnemyUnit.Active.Count; i++)
        {
            EnemyUnit.Active[i].transform.position += Vector3.down * delta;
        }
    }

    bool ShouldSpawnNextRow()
    {
        var arr = FindObjectsOfType<EnemyUnit>();
        if (arr.Length == 0) return true;

        float highest = float.MinValue;
        foreach (var s in arr)
            highest = Mathf.Max(highest, s.transform.position.y);

        return highest <= (nextSpawnY - rowSpacing);
    }

    void SpawnRow()
    {
        var layout = RowLayoutSolver.SolveRow(
            shapes,
            gridWidth,
            difficulty.currentDifficulty,
            difficulty.GetFillChance()
        );

        foreach (var entry in layout.shapes)
        {
            var data = entry.data;
            int slotX = entry.slotX;

            float worldX = leftEdge + slotX * gridSize;

            var obj = Instantiate(data.prefab);
            obj.Init(data);
            obj.transform.position = new Vector3(worldX, nextSpawnY, 0);
        }

        nextSpawnY += layout.maxHeight * gridSize;
        difficulty.OnRowSpawned();
    }

}

public class ShapeInstance
{
    public List<EnemyUnit> units = new List<EnemyUnit>();

    public float GetLowestY()
    {
        float lowest = Mathf.Infinity;
        foreach (var u in units)
        {
            if (u != null)
                lowest = Mathf.Min(lowest, u.transform.position.y);
        }
        return lowest;
    }
}

public class RowInstance
{
    public List<ShapeInstance> shapes = new List<ShapeInstance>();

    public float GetLowestY()
    {
        float lowest = Mathf.Infinity;
        foreach (var s in shapes)
        {
            float y = s.GetLowestY();
            if (y < lowest) lowest = y;
        }
        return lowest;
    }
}

