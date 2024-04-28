using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sim10UpDiamondSpawnManager : MonoBehaviour
{
    private static readonly Vector3 spawnPosMiddle = new Vector3(0f, -3.56f, 0f);

    private static readonly Vector3 spawnPosLeft = new Vector3(-1.2f, -3.56f, 0f);

    private static readonly Vector3 spawnPosRight = new Vector3(1.2f, -3.56f, 0f);

    [SerializeField]
    private GameObject upDiamondPrefab;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnMiddleDiamond), 2f, 4f);
        InvokeRepeating(nameof(SpawnSideDiamonds), 4f, 4f);
    }

    private void SpawnMiddleDiamond()
    {
        Instantiate(upDiamondPrefab, spawnPosMiddle, Quaternion.identity);
    }

    private void SpawnSideDiamonds()
    {
        Instantiate(upDiamondPrefab, spawnPosLeft, Quaternion.identity);
        Instantiate(upDiamondPrefab, spawnPosRight, Quaternion.identity);
    }
}
