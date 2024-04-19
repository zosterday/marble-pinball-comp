using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sim9UpDiamondSpawnManager : MonoBehaviour
{
    private static readonly Vector3 spawnPosTop = new Vector3(0f, 4.5f, 0f);

    private static readonly Vector3 spawnPosLeft = new Vector3(-2f, -3.1f, 0f);

    private static readonly Vector3 spawnPosRight = new Vector3(2f, -3.1f, 0f);

    [SerializeField]
    private GameObject upDiamondPrefab;

    [SerializeField]
    private GameObject TopDiamondPrefab;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnDiamond), 2.5f, 1.3f);
        Invoke(nameof(SpawnTopDiamond), 4f);
        Invoke(nameof(SpawnTopDiamond), 5.5f);
    }

    private void SpawnDiamond()
    {
        Instantiate(upDiamondPrefab, spawnPosLeft, Quaternion.identity);
        Instantiate(upDiamondPrefab, spawnPosRight, Quaternion.identity);
    }

    private void SpawnTopDiamond()
    {
        Instantiate(TopDiamondPrefab, spawnPosTop, Quaternion.identity);
    }
}
