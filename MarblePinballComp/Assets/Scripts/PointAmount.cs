using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAmount : MonoBehaviour
{
    public int Points => points;

    [SerializeField]
    private int points = 1;
}
