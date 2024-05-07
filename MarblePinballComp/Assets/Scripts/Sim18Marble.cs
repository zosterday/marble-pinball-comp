using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sim18Marble : MonoBehaviour
{
    private const string DeathBarrierTag = "DeathBarrier";

    private const string BottomBarrierTag = "BottomMarbleBound";

    private Rigidbody2D rb;

    private Renderer renderer;

    public string Username { get; set; }

    private int id;

    public int Id => id;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        renderer = GetComponent<Renderer>();
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    private void Update()
    {
        if (Sim18GameManager.Instance.IsSimActive)
        {
            rb.constraints = RigidbodyConstraints2D.None;
        }
        if (!Sim18GameManager.Instance.IsSimActive)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(DeathBarrierTag))
        {
            Sim18GameManager.Instance.CheckForGameOver(id);
            Destroy(gameObject);
            return;
        }

        if (collision.CompareTag(BottomBarrierTag))
        {
            rb.velocity = new Vector2();
            transform.position = new Vector3(
                Random.Range(Sim18GameManager.Instance.XSpawnMin, Sim18GameManager.Instance.XSpawnMax),
                Random.Range(Sim18GameManager.Instance.YSpawnMin, Sim18GameManager.Instance.YSpawnMax),
                1f);
            return;
        }
    }

    public void AssignId(int id)
    {
        this.id = id;
    }
}
