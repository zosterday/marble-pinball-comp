using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sim17UpDiamondSpawnManager : MonoBehaviour
{
    private static readonly Vector3 spawnPosLeft = new Vector3(-2.55f, -3.1f, 0f);
    private static readonly Vector3 spawnPosRight = new Vector3(2.55f, -3.1f, 0f);

    [SerializeField]
    private GameObject upDiamondPrefab;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnDiamonds), 0f, 1.85f);
    }

    private void SpawnDiamonds()
    {
        Instantiate(upDiamondPrefab, spawnPosLeft, Quaternion.identity);
        Instantiate(upDiamondPrefab, spawnPosRight, Quaternion.identity);
    }
}
