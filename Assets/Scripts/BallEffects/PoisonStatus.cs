using UnityEngine;

public class PoisonStatus : MonoBehaviour
{
    PoisonBallEffect data;
    EnemyUnit enemy;

    int stacks = 1;
    float timer;

    public void Initialize(PoisonBallEffect effect)
    {
        data = effect;
        enemy = GetComponent<EnemyUnit>();
        timer = data.duration;

        InvokeRepeating(nameof(Tick), 0f, data.tickRate);
    }

    public void AddStack()
    {
        stacks = Mathf.Min(stacks + 1, data.maxStacks);
        timer = data.duration; // refresh duration
    }

    void Tick()
    {
        if (timer <= 0f)
        {
            CancelInvoke();
            Destroy(this);
            return;
        }

        enemy.TakeDamage(data.damagePerTick * stacks);
        timer -= data.tickRate;
    }
}
