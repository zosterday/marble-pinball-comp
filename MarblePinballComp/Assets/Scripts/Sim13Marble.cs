using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Sim13Marble : MonoBehaviour
{
    private const float MaxSpeed = 10f;
    private const float SizeReduction = 0.01f;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        //rb.velocity = Vector3.ClampMagnitude(rb.velocity, MaxSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ball"))
        {
            Sim13GameManager.Instance.IncrementSpawnCount();
        }
    }
}
