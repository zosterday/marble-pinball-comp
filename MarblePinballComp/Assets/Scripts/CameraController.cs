using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 2.0f; // Adjust the speed as needed

    private Vector3 targetPosition;
    private bool isMoving = false;

    private void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (transform.position == targetPosition)
            {
                isMoving = false;
            }
        }
    }

    public void MoveCameraDown(float moveAmount)
    {
        targetPosition = transform.position - Vector3.up * moveAmount;
        isMoving = true;
    }
}
