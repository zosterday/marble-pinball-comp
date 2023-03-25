using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marble : MonoBehaviour
{
    private const string PointSquareTag = "PointSquare";
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
            return;
        }

        var pointSquare = collision.GetComponent<PointAmount>();

        //Call gameManager method to add points to this color

        Destroy(gameObject);
    }
}
