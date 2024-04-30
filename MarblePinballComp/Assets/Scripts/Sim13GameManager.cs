using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sim13GameManager : MonoBehaviour
{
    private const float XSpawnMin = -1f;

    private const float XSpawnMax = 1f;

    private const float YSpawnMin = 1f;

    private const float YSpawnMax = 1.5f;

    private int lastCounter;

    private int counter;

    [SerializeField]
    private GameObject marblePrefab;

    private static Sim13GameManager instance;

    public static Sim13GameManager Instance
    {
        get
        {
            if (instance is null)
            {
                throw new NullReferenceException("Instance of Sim13GameManager is null");
            }

            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (counter >= lastCounter + 2)
        {
            SpawnMarble();
            lastCounter = counter;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ColorUtility.TryParseHtmlString("#00F7FF", out var firstColor);
        ColorUtility.TryParseHtmlString("#FF8900", out var secondColor);
        SpawnMarble(new Vector3(-0.5f, 0.5f, 0f), firstColor);
        SpawnMarble(new Vector3(0.5f, 1.5f, 0f), secondColor);
    }

    private void SpawnMarble()
    {
        var spawnPos = new Vector3(UnityEngine.Random.Range(XSpawnMin, XSpawnMax), UnityEngine.Random.Range(YSpawnMin, YSpawnMax), 1f);
        var marble = Instantiate(marblePrefab, spawnPos, Quaternion.identity);
        var color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
        SetupMarble(marble, color);
    }

    private void SpawnMarble(Vector3 spawnPos, Color color)
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

    public void IncrementSpawnCount()
    {
        counter++;
    }
}
