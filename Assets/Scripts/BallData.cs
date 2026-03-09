using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ball/Ball Data")]
public class BallData : ScriptableObject
{
    public string ballName;
    public Ball prefab;

    [Header("Stats")]
    public float speed = 8f;
    public int damage = 1;

    [Header("Special")]
    public BallEffect effect;
}

public abstract class BallEffect : ScriptableObject
{
    public virtual void OnHit(Ball ball, EnemyUnit enemy, Collision2D col) { }
}
