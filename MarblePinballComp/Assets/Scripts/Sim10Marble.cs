using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sim10Marble : MonoBehaviour
{
    private Rigidbody2D rb;

    private Renderer renderer;

    private ParticleSystem particleSystem;

    private bool isExploded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        renderer = GetComponent<Renderer>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
        particleSystem.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!Sim4GameManager.Instance.IsSimActive)
        {
            if (!isExploded)
            {
                Invoke(nameof(Explode), 0.5f);
            }
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("BottomMarbleBound"))
        {
            return;
        }

        rb.velocity = new Vector2();
        transform.position = new Vector3(
            UnityEngine.Random.Range(Sim4SpawnManager.Instance.XSpawnMin, Sim4SpawnManager.Instance.XSpawnMax),
            Sim4SpawnManager.Instance.YSpawn,
            1f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("StartingBox"))
        {
            rb.AddForce(transform.up * 45f);
            return;
        }

        if (collision.collider.CompareTag("DestroyableBlock"))
        {
            var block = collision.collider.GetComponent<Sim10DestroyableBlock>();
            if (block.IsBroken)
            {
                return;
            }
            StartCoroutine(block.BreakBlock(renderer.material.color));
            return;
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
}
