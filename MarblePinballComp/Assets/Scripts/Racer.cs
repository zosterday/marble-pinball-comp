using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racer : MonoBehaviour
{
    [SerializeField]
    private string racerName;

    [SerializeField]
    private AudioClip bounceSound;

    private AudioSource playerAudio;

    private float speed = 1.85f;

    private Rigidbody2D rb;

    private Vector2 lastVelocity;

    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        var dir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        rb.velocity = dir.normalized * speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Sim23GameManager.Instance.IsEnded)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            return;
        }
        lastVelocity = rb.velocity;
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, 1f) * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (!collision.collider.CompareTag("Obstacle"))
        //{
        //    return;
        //}

        playerAudio.PlayOneShot(bounceSound, 1f);
        Sim23GameManager.Instance.PlayCollisionParticle(collision.GetContact(0).point);
        Vector2 normal = collision.contacts[0].normal;
        if (collision.rigidbody.velocity.magnitude < 0.001f)
        {
            rb.velocity = Vector2.Reflect(lastVelocity.normalized, normal);
            return;
        }

        var relativeVelocity = rb.velocity - collision.rigidbody.velocity;
        var newDirection = Vector3.Reflect(rb.velocity.normalized, collision.contacts[0].normal);
        rb.velocity = newDirection * relativeVelocity.magnitude;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("RaceWinZone"))
        {
            Sim23GameManager.Instance.EndRace(racerName);
        }
    }
}

