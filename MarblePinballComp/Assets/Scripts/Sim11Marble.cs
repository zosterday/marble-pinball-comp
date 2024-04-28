using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sim11Marble : MonoBehaviour
{
    private Rigidbody2D rb;

    private Renderer renderer;

    private ParticleSystem particleSystem;

    private int id;

    public int Id => id;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        renderer = GetComponent<Renderer>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
        particleSystem.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (!Sim11GameManager.Instance.IsSimActive)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("DeathBall"))
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            Invoke(nameof(CheckGameOver), 0.68f);
            Explode();
            return;
        }
    }

    private void CheckGameOver()
    {
        Sim11GameManager.Instance.CheckForGameOver(id);
    }

    public void Explode()
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
