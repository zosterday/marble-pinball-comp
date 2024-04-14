using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sim4SpawnManager : MonoBehaviour
{
    public readonly float XSpawnMin = -2.45f;

    public readonly float XSpawnMax = 2.45f;

    public readonly float YSpawn = 4.55f;

    private static Sim4SpawnManager instance;

    [SerializeField]
    private GameObject marblePrefab;

    public static Sim4SpawnManager Instance
    {
        get
        {
            if (instance is null)
            {
                throw new System.InvalidOperationException("Instance of Sim4SpawnManager is null");
            }

            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    public void SpawnMarble(Color color)
    {
        var spawnPos = new Vector3(Random.Range(XSpawnMin, XSpawnMax), YSpawn, 1f);
        var marble = Instantiate(marblePrefab, spawnPos, Quaternion.identity);
        SetupMarble(marble, color);
    }

    public void SpawnMarble(Vector3 spawnPos, Color color)
    {
        var marble = Instantiate(marblePrefab, spawnPos, Quaternion.identity);
        SetupMarble(marble, color);
    }

    private void SetupMarble(GameObject marble, Color color)
    {
        var renderer = marble.GetComponent<Renderer>();
        renderer.material.color = color;
        var trailRenderer = marble.GetComponent<TrailRenderer>();
        trailRenderer.startColor = color;
        var endColor = color;
        endColor.a = 0f;
        trailRenderer.endColor = endColor;
    }
}
