using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sim21Marble : MonoBehaviour
{
    private const float MaxSpeed = 6.5f;

    private Rigidbody2D rb;
    private TrailRenderer trail;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        trail = GetComponent<TrailRenderer>();
        InvokeRepeating(nameof(ReduceSize), 0.5f, 0.01f);
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, MaxSpeed);
    }

    private void ReduceSize()
    {
        transform.localScale *= 0.999f;
        trail.widthMultiplier *= 0.9995f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ball"))
        {
            transform.localScale += new Vector3(0.12f, 0.12f);
            trail.widthMultiplier += 0.4f;

            Sim21GameManager.Instance.PlayCollisionParticle(collision.GetContact(0).point);
        }
    }
}
