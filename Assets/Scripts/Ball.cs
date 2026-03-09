using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Ball : MonoBehaviour
{
    public BallEffect eff;

    Rigidbody2D rb;
    BallLauncher owner;
    BallData data;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Launch(BallLauncher launcher, BallData ballData, Vector2 dir)
    {
        owner = launcher;
        data = ballData;

        rb.velocity = dir.normalized * data.speed;
    }

    public void Init(BallData data)
    {
        this.data = data;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.collider.TryGetComponent(out EnemyUnit enemy))
            return;

        enemy.TakeDamage(data.damage);
        data.effect?.OnHit(this, enemy, col);
    }

    public void Return()
    {
        rb.velocity = Vector2.zero;
        owner.OnBallReturned(data);
        Destroy(gameObject); // pool later
    }

    public BallData getData()
    {
        return data;
    }
}
