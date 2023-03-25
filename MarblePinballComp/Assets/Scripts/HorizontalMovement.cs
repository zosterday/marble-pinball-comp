using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovement : MonoBehaviour
{
    private const float Speed = 1.5f;

    private Vector3 originalPosition;

    [SerializeField]
    private bool isMovingRight  = true;

    [SerializeField]
    private float movementRange = 1.25f;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMovingRight)
        {
            MoveRight();
            if (transform.position.x >= originalPosition.x + movementRange)
            {
                isMovingRight = false;
            }
        }
        else
        {
            MoveLeft();
            if (transform.position.x <= originalPosition.x - movementRange)
            {
                isMovingRight = true;
            }
        }
    }

    private void MoveRight()
    {
        var targetPosition = new Vector3(transform.position.x + Speed, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);
    }

    private void MoveLeft()
    {
        var targetPosition = new Vector3(transform.position.x - Speed, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);
    }
}
