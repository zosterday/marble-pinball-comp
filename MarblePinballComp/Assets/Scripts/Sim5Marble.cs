using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Sim5Marble : MonoBehaviour
{
    private Rigidbody2D rb;

    private Renderer renderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("BottomMarbleBound"))
        {
            return;
        }

        rb.velocity = new Vector2();
        transform.position = new Vector3(
            UnityEngine.Random.Range(Sim5SpawnManager.Instance.XSpawnMin, Sim5SpawnManager.Instance.XSpawnMax),
            Sim5SpawnManager.Instance.YSpawn,
        1f);
    }
}
