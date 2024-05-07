using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Sim8GameManager : MonoBehaviour
{
    public readonly float XSpawnMin = 1.5f;

    public readonly float XSpawnMax = -1.51f;

    public readonly float YSpawnMin = 5f;

    public readonly float YSpawnMax = 5.45f;

    private const float WinnerScaleFactor = 120f;

    public bool IsSimActive { get; private set; }

    public static Sim8GameManager Instance
    {
        get
        {
            if (instance is null)
            {
                throw new NullReferenceException("Instance of Sim8GameManager is null");
            }

            return instance;
        }
    }

    private static Sim8GameManager instance;

    [SerializeField]
    private int spawnCount = 100;

    [SerializeField]
    private GameObject leaderboardIconPrefab;

    [SerializeField]
    private GameObject marblePrefab;

    [SerializeField]
    private GameObject endGamePanel;

    private void Awake()
    {
        IsSimActive = false;
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnMarbles();
        IsSimActive = true;
    }

    private void SpawnMarbles()
    {
        for (var i = 0; i < spawnCount; i++)
        {
            var spawnPos = new Vector3(UnityEngine.Random.Range(XSpawnMin, XSpawnMax),
                UnityEngine.Random.Range(YSpawnMin, YSpawnMax),
                1f);
            var marble = Instantiate(marblePrefab, spawnPos, Quaternion.identity);
            var marbleScript = marble.GetComponent<Sim8Marble>();
            marbleScript.AssignId(i);

            var color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
            var renderer = marble.GetComponent<Renderer>();
            renderer.material.color = color;
            var trailRenderer = marble.GetComponent<TrailRenderer>();
            trailRenderer.startColor = color;
            var endColor = color;
            endColor.a = 0f;
            trailRenderer.endColor = endColor;
        }
    }

    private void DisplayEndGamePanel(Color color)
    {
        endGamePanel.SetActive(true);

        var leaderboardIcon = Instantiate(leaderboardIconPrefab, Vector3.zero, Quaternion.identity);
        leaderboardIcon.transform.SetParent(endGamePanel.transform);
        leaderboardIcon.transform.localScale = new Vector3(WinnerScaleFactor, WinnerScaleFactor);

        var iconRenderer = leaderboardIcon.GetComponent<Renderer>();
        iconRenderer.material.color = color;
    }

    public void CheckForGameOver(int loserId)
    {
        var marblesRemaining = FindObjectsOfType<Sim8Marble>().ToArray();
        if (marblesRemaining.Length != 2)
        {
            return;
        }

        foreach (var marble in marblesRemaining)
        {
            if (marble.Id != loserId)
            {
                var lastMarbleRenderer = marble.GetComponent<Renderer>();
                IsSimActive = false;
                DisplayEndGamePanel(lastMarbleRenderer.material.color);
            }
        }
    }
}
