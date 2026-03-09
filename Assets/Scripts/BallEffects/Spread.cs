using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ball Effects/Split")]
public class SplitBallBehaviour : BallEffect
{
    [Header("Split Settings")]
    public int splitCount = 3;
    public float angleSpread = 30f;   // degrees
    public bool destroyOriginal = true;

    [Header("Child Ball")]
    public BallData childBall;
    public float speedMultiplier = 0.8f;
    public int damageOverride = -1; // -1 = use child definition

    public override void OnHit(Ball ball, EnemyUnit enemy, Collision2D col)
    {
        Vector2 baseDir = ball.GetComponent<Rigidbody2D>().velocity.normalized;
        ball.Return();

        float startAngle = -angleSpread * 0.5f;
        float step = angleSpread / (splitCount - 1);

        for (int i = 0; i < splitCount; i++)
        {
            float angle = startAngle + step * i;
            Vector2 dir = Quaternion.Euler(0, 0, angle) * baseDir;

            SpawnChild(ball.transform.position, dir);
        }

        if (destroyOriginal)
        {
            
            Destroy(ball.gameObject);
        }
    }

    void SpawnChild(Vector2 pos, Vector2 dir)
    {
        var obj = Instantiate(childBall.prefab, pos, Quaternion.identity);
        var newBall = obj.GetComponent<Ball>();

        newBall.Init(childBall);

        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        rb.velocity = dir.normalized * childBall.speed;
    }
}