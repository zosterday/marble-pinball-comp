using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sim21GameManager : MonoBehaviour
{
    private const float XSpawnMin = -1f;

    private const float XSpawnMax = 1f;

    private const float YSpawnMin = 1f;

    private const float YSpawnMax = 1.5f;

    [SerializeField]
    private GameObject marblePrefab;

    [SerializeField]
    private ParticleSystem collisionParticle;

    private static Sim21GameManager instance;

    public static Sim21GameManager Instance
    {
        get
        {
            if (instance is null)
            {
                throw new NullReferenceException("Instance of Sim21GameManager is null");
            }

            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
        Invoke(nameof(StartSim), 2f);
    }

    // Start is called before the first frame update
    void StartSim()
    {
        var color = Color.red;
        ColorUtility.TryParseHtmlString("#00F7FF", out color);
        SpawnMarble(new Vector3(-1f, 0.5f, 0f), color);
        ColorUtility.TryParseHtmlString("#FF0E00", out color);
        SpawnMarble(new Vector3(1f, 0.5f, 0f), color);
        //ColorUtility.TryParseHtmlString("#03FF00", out color);
        //SpawnMarble(new Vector3(0f, 0.5f, 0f), color);
        //ColorUtility.TryParseHtmlString("#E700FF", out color);
        //SpawnMarble(new Vector3(-0.5f, 1f, 0f), color);
        //ColorUtility.TryParseHtmlString("#FFAD00", out color);
        //SpawnMarble(new Vector3(0.5f, 1f, 0f), color);
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

    public void PlayCollisionParticle(Vector3 position)
    {
        collisionParticle.transform.position = position;
        collisionParticle.Play();
    }
}
