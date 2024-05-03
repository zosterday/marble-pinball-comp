using UnityEngine;

public class MoveThroughCenter : MonoBehaviour
{
    private float speed = 2f; 

    private Vector3 movementDirection;

    private void Start()
    {
        movementDirection = Vector3.zero - transform.position;
        movementDirection.Normalize();
    }

    private void Update()
    {
        transform.position += movementDirection * speed * Time.deltaTime;
    }
}
