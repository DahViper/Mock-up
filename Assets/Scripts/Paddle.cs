using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float speed = 10f;
    public float boundaryX = 8f; // Adjust based on your screen size

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(mousePos.x, -boundaryX, boundaryX);
        transform.position = pos;
    }
}
