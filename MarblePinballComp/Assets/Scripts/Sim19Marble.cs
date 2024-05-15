using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sim19Marble : MonoBehaviour
{
    private const float MaxSpeed = 6.5f;

    private Rigidbody2D rb;
    private TrailRenderer trail;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        trail = GetComponent<TrailRenderer>();
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, MaxSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ball"))
        {
            transform.localScale *= 1.07f;
            trail.widthMultiplier *= 1.07f;
        }
    }
}
