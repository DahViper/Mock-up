using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Ball Effects/Explosion")]
public class ExplosionEffect : BallEffect
{
    public float radius = 2f;
    public int damage = 2;

    public override void OnHit(Ball ball, EnemyUnit enemy, Collision2D col)
    {
        var enemies = EnemyUnit.Active.ToList();
        foreach (var e in enemies)
        {
            if (Vector2.Distance(enemy.transform.position, e.transform.position) <= radius)
                e.TakeDamage(damage);
        }
    }
}
