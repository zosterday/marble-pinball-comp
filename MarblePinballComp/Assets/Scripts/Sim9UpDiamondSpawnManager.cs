using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sim9UpDiamondSpawnManager : MonoBehaviour
{
    private static readonly Vector3 spawnPosLeft = new Vector3(-2f, -3.1f, 0f);

    private static readonly Vector3 spawnPosRight = new Vector3(2f, -3.1f, 0f);

    [SerializeField]
    private GameObject upDiamondPrefab;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnDiamond), 0f, 1.3f);
    }

    private void SpawnDiamond()
    {
        Instantiate(upDiamondPrefab, spawnPosLeft, Quaternion.identity);
        Instantiate(upDiamondPrefab, spawnPosRight, Quaternion.identity);
    }
}
