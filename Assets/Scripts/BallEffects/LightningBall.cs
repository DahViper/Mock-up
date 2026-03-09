using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ball Effects/Chain Lightning")]
public class ChainLightningEffect : BallEffect
{
    public int chainCount = 3;
    public float range = 2.5f;
    public float damageFalloff = 0.7f;

    public override void OnHit(Ball ball, EnemyUnit start, Collision2D col)
    {
        var hit = new HashSet<EnemyUnit> { start };
        var current = start;
        float damage = ball.getData().damage;

        for (int i = 0; i < chainCount; i++)
        {
            var next = FindNext(current, hit);
            if (!next) break;

            damage *= damageFalloff;
            next.TakeDamage(Mathf.CeilToInt(damage));
            //LightningVFX.Spawn(current.transform.position, next.transform.position);

            hit.Add(next);
            current = next;

            Debug.Log("ZAPPP!!!");
        }
    }

    EnemyUnit FindNext(EnemyUnit from, HashSet<EnemyUnit> ignore)
    {
        EnemyUnit best = null;
        float bestDist = float.MaxValue;

        foreach (var e in EnemyUnit.Active)
        {
            if (ignore.Contains(e)) continue;

            float d = Vector2.Distance(from.transform.position, e.transform.position);
            if (d < bestDist)
            {
                bestDist = d;
                best = e;
            }
        }

        return best;
    }
}
