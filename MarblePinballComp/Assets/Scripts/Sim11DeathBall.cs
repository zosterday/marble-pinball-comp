using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sim11DeathBall : MonoBehaviour
{
    private const float SizeIncrease = 0.0165f;

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
        if (!Sim11GameManager.Instance.IsSimActive)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        lastVelocity = rb.velocity;
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, speed);
        if (rb.velocity.magnitude < speed)
        {
            var dir = Vector3.zero - transform.position;
            rb.velocity = dir.normalized * speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!Sim11GameManager.Instance.IsSimActive)
        {
            return;
        }
        if (collision.gameObject.CompareTag("Ball"))
        {
            //Make size bigger and destroy the other ball
            var curScale = transform.localScale;
            transform.localScale = new Vector3(curScale.x + SizeIncrease,
                                               curScale.y + SizeIncrease,
                                               curScale.z + SizeIncrease);
            speed += 0.028f;
        }
        Vector2 normal = collision.contacts[0].normal;
        rb.velocity = Vector2.Reflect(lastVelocity.normalized, normal);
    }
}

