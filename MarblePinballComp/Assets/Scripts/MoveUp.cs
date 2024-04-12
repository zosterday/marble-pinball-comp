using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUp : MonoBehaviour
{
    private const float Speed = 1.3f;

    private const string UpperBoundTag = "UpperBound";

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        var targetPosition = new Vector3(transform.position.x, transform.position.y + Speed, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(UpperBoundTag))
        {
            return;
        }
        Destroy(gameObject);
    }
}
