using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "PoisonBallEffect",
    menuName = "Ball Effects/Poison"
)]
public class PoisonBallEffect : BallEffect
{
    public float duration = 3f;
    public float tickRate = 0.5f;
    public int damagePerTick = 1;
    public int maxStacks = 5;

    public override void OnHit(Ball ball, EnemyUnit enemy, Collision2D col)
    {
        PoisonStatus poison = enemy.GetComponent<PoisonStatus>();

        if (poison == null)
        {
            poison = enemy.gameObject.AddComponent<PoisonStatus>();
            poison.Initialize(this);
        }
        else
        {
            poison.AddStack();
        }
    }
}
