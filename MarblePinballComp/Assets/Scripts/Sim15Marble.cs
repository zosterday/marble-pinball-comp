using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Sim15Marble : MonoBehaviour
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
    }

    private void Update()
    {
        if (!Sim15GameManager.Instance.IsSimActive)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, MaxSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            Explode();
            Destroy(collision.gameObject);
            Sim15GameManager.Instance.CheckForGameOver(id);
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
