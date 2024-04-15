using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sim6UpDiamondSpawnManager : MonoBehaviour
{
    private static readonly Vector3 spawnPos = new Vector3(1.95f, -3.1f, 0f);

    [SerializeField]
    private GameObject upDiamondPrefab;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnDiamond), 0f, 1.3f);
    }

    private void SpawnDiamond()
    {
        Instantiate(upDiamondPrefab, spawnPos, Quaternion.identity);
    }
}
