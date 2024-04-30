using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Sim12Marble : MonoBehaviour
{
    private const float MaxSpeed = 10f;
    private const float SizeReduction = 0.01f;

    private Rigidbody2D rb;
    private ParticleSystem particleSystem;

    private int currentHitCount;

    private int id;

    [SerializeField]
    private string colorString;

    public int Id => id;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
        particleSystem.gameObject.SetActive(false);

        var renderer = GetComponent<SpriteRenderer>();
        var ballColor = renderer.color;
        var trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.startColor = ballColor;
        var endColor = ballColor;
        endColor.a = 0f;
        trailRenderer.endColor = endColor;
    }

    private void Update()
    {
        if (!Sim12GameManager.Instance.IsSimActive)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, MaxSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ball"))
        {
            var curScale = transform.localScale;
            transform.localScale = new Vector3(curScale.x - SizeReduction,
                                               curScale.y - SizeReduction,
                                               curScale.z - SizeReduction);
            rb.mass--;
            currentHitCount++;
            Sim12GameManager.Instance.TakeLife(colorString);
            if (currentHitCount >= 15)
            {
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                Explode();
            }
        }
    }

    private void Explode()
    {
        particleSystem.gameObject.SetActive(true);
        particleSystem.Play();
        Invoke(nameof(SetInactive), 0.65f);
    }

    private void SetInactive()
    {
        gameObject.SetActive(false);
    }

    public void AssignId(int id)
    {
        this.id = id;
    }
}
