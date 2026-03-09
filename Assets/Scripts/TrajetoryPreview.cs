using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class TrajectoryPreview : MonoBehaviour
{
    public Transform firePoint;

    public int maxBounces = 5;
    public float maxDistance = 30f;
    public LayerMask collisionMask;

    LineRenderer lr;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    public void Show(Vector2 direction)
    {
        List<Vector3> points = new List<Vector3>();

        Vector2 pos = firePoint.position;
        Vector2 dir = direction.normalized;

        
        points.Add(pos);

        float remainingDistance = maxDistance;

        for (int i = 0; i < maxBounces; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(
                pos,
                dir,
                remainingDistance,
                collisionMask
            );

            if (hit.collider == null)
            {
                points.Add(pos + dir * remainingDistance);
                break;
            }

            points.Add(hit.point);

            remainingDistance -= Vector2.Distance(pos, hit.point);
            pos = hit.point + dir * 0.01f;

            dir = Vector2.Reflect(dir, hit.normal);
        }

        lr.positionCount = points.Count;
        lr.SetPositions(points.ToArray());
    }

    public void Hide()
    {
        lr.positionCount = 0;
    }
}
