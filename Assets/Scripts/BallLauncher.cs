using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BallLauncher : MonoBehaviour
{
    public static BallLauncher Instance;
    public Transform firePoint;
    public float shootInterval = 0.12f;
    public TrajectoryPreview trajectory;

    public List<BallData> inventory = new(); // inventory

    int activeBalls;
    bool isShooting;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        Vector2 dir = GetAimDirection();
        dir.y = Mathf.Max(dir.y, 0.1f);
        dir.Normalize();

        trajectory.Show(dir);
        if (Input.GetMouseButtonDown(0) && CanShoot())
        {
            
            StartCoroutine(ShootSequence(dir));
        }
    }

    bool CanShoot()
    {
        return !isShooting && inventory.Count > 0;
    }

    IEnumerator ShootSequence(Vector2 dir)
    {
        isShooting = true;

        while (inventory.Count > 0)
        {
            BallData data = inventory[0];
            inventory.RemoveAt(0);

            SpawnBall(data, dir);

            yield return new WaitForSeconds(shootInterval);
        }

        isShooting = false;
    }

    void SpawnBall(BallData data, Vector2 dir)
    {
        Ball ball = Instantiate(data.prefab, firePoint.position, Quaternion.identity);
        ball.Launch(this, data, dir);
    }

    public void OnBallReturned(BallData data)
    {
        inventory.Add(data);
    }

    Vector2 GetAimDirection()
    {
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return (mouse - firePoint.position).normalized;
    }
}
