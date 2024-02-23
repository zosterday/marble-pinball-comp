using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marble : MonoBehaviour
{
    private const string PointSquareTag = "PointSquare";

    [SerializeField]
    private ParticleSystem marbleCollisionParticle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(PointSquareTag))
        {
            //marbleCollisionParticle.Play();
            return;
        }

        var points = collision.GetComponent<PointAmount>().Points;
        var color = GetComponent<SpriteRenderer>().material.color;
        GameManager.Instance.UpdateScore(color, points);

        Destroy(gameObject);
    }
}
