using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Sim16Marble : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("CircleBound"))
        {
            Sim16GameManager.Instance.IncrementBounceCounter();
        }
    }
}
