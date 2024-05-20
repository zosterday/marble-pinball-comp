using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sim20Marble : MonoBehaviour
{
    private const float MaxSpeed = 6.6f;

    private Rigidbody2D rb;

    private float timeUntilFreeze = 2.34f;
    private bool isFrozen;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (isFrozen)
        {
            return;
        }
        timeUntilFreeze -= Time.deltaTime;
        if (timeUntilFreeze <= 0f)
        {
            if (transform.position.magnitude >= 2.01f)
            {
                return;
            }
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            isFrozen = true;
            Sim20GameManager.Instance.SpawnMarble();
        }
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, MaxSpeed);
    }
}
