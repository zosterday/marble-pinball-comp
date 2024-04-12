using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDiamondSpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject upDiamondPrefab;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnDiamond), 0f, 1.3f);
    }

    private void SpawnDiamond()
    {
        Instantiate(upDiamondPrefab);
    }
}
