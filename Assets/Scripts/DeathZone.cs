using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            //GameManager.Instance.LoseLife();

            if (other.TryGetComponent(out Ball ball))
            {
                ball.Return();
            }
        }
        else Destroy(other.gameObject);
    }
}
