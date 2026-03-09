using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : MonoBehaviour
{
    public EnemyShapeData data;
    private int hp;
    private int orb;

    public static List<EnemyUnit> Active = new List<EnemyUnit>();

    SpriteRenderer sr;
    Color defaultColor;
    Coroutine flashRoutine;

    void OnEnable() => Active.Add(this);
    void OnDisable() => Active.Remove(this);

    public void Init(EnemyShapeData d)
    {
        data = d;
        hp = d.health;
        orb = d.exp;
    }

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        defaultColor = sr.color;
    }
    public void TakeDamage(int dmg)
    {
        hp -= dmg;
        FlashWhite();

        if (hp <= 0)
        {
            GameManager.Instance.AddExp(orb);
            GameManager.Instance.PlayDeath(transform.position);
            Die();
        }
    }

    void FlashWhite()
    {
        if (flashRoutine != null)
            StopCoroutine(flashRoutine);

        flashRoutine = StartCoroutine(FlashCoroutine());
    }

    IEnumerator FlashCoroutine()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.06f);
        sr.color = defaultColor;
        flashRoutine = null;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}

