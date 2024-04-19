using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sim9Marble : MonoBehaviour
{
    private Rigidbody2D rb;

    private Renderer renderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        renderer = GetComponent<Renderer>();
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    private void Update()
    {
        if (!Sim9GameManager.Instance.IsSimActive)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MoveCameraDownTrigger"))
        {
            Sim9GameManager.Instance.MoveMainCamera();
        }
        if (collision.CompareTag("BottomMarbleBound"))
        {
            Sim9GameManager.Instance.EndGame(renderer.material.color);
        }
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
            var block = collision.collider.GetComponent<Sim6DestroyableBlock>();
            if (block.IsBroken)
            {
                return;
            }
            StartCoroutine(block.BreakBlock(renderer.material.color));
            return;
        }
    }
}
