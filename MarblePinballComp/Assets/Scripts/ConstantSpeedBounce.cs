using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantSpeedBounce : MonoBehaviour
{
    //TODO tomorrow: need to make it so if stops moving then it gets a new force to
    //make it move again. Happens if it gets hit like twice at the near same time

    [SerializeField]
    private float speed = 0.3f;

    private Rigidbody2D rb;

    private Vector2 lastVelocity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        var dir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        rb.velocity = dir.normalized * speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        lastVelocity = rb.velocity;
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 normal = collision.contacts[0].normal;
        rb.velocity = Vector2.Reflect(lastVelocity.normalized, normal);
    }
}

