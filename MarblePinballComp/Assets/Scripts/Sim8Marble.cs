using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sim8Marble : MonoBehaviour
{
    private const string DeathBarrierTag = "DeathBarrier";

    private const string BottomBarrierTag = "BottomMarbleBound";

    private Rigidbody2D rb;

    private int id;

    public int Id => id;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!Sim8GameManager.Instance.IsSimActive)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(DeathBarrierTag))
        {
            Sim8GameManager.Instance.CheckForGameOver(id);
            Destroy(gameObject);            
            return;
        }

        if (collision.CompareTag(BottomBarrierTag))
        {
            rb.velocity = new Vector2();
            transform.position = new Vector3(
                Random.Range(Sim8GameManager.Instance.XSpawnMin, Sim8GameManager.Instance.XSpawnMax),
                Random.Range(Sim8GameManager.Instance.YSpawnMin, Sim8GameManager.Instance.YSpawnMax),
                1f);
            return;
        }
    }

    public void AssignId(int id)
    {
        this.id = id;
    }
}
