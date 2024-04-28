using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sim11DeathBall : MonoBehaviour
{
    private const float SizeIncrease = 0.035f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            //Make size bigger and destroy the other ball
            var curScale = transform.localScale;
            transform.localScale = new Vector3(curScale.x + SizeIncrease,
                                               curScale.y + SizeIncrease,
                                               curScale.z + SizeIncrease);
            return;
        }
    }
}

