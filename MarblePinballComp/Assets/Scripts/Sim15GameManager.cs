using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Sim15GameManager : MonoBehaviour
{
    private const float ObstacleSpawnDistance = 2.3f;

    private const float WinnerScaleFactor = 120f;

    public bool IsSimActive { get; private set; }

    private static Sim15GameManager instance;

    private readonly Dictionary<Color, int> scoresByColor = new();

    private readonly Dictionary<Color, GameObject> leaderboardIconsByColor = new();

    private readonly List<GameObject> leaderboardIcons = new();

    private readonly List<Vector3> startingMarbleSpawnPositions = new()
    {
        new (-1.35f, 1f, 0f),
        new (-0.3f, 1f, 0f),
        new (0.3f, 1f, 0f),
        new (1.35f, 1f, 0f),
        new (-1f, 1.5f, 0f),
        new (1f, 1.5f, 0f),
        new (-0.5f, 1.5f, 0f),
        new (0.5f, 1.5f, 0f)
    };

    private readonly Vector3 screenOffset = new Vector3(70f, 100f, 0f);

    private readonly Vector3 placementYOffset = new Vector3(0f, -50f, 0f);

    [SerializeField]
    private GameObject ObstacleBlockPrefab;

    [SerializeField]
    private List<Color> colors;

    [SerializeField]
    private GameObject leaderboardIconPrefab;

    [SerializeField]
    private GameObject endGamePanel;

    [SerializeField]
    private GameObject marblePrefab;

    public List<Color> Colors
    {
        get
        {
            return colors;
        }
    }

    public static Sim15GameManager Instance
    {
        get
        {
            if (instance is null)
            {
                throw new NullReferenceException("Instance of Sim15GameManager is null");
            }

            return instance;
        }
    }

    private void Awake()
    {
        IsSimActive = false;
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnStartingMarbles();
        IsSimActive = true;

        InvokeRepeating(nameof(SpawnObstacle), 2f, 1.5f);
    }

    public void CheckForGameOver(int loserId)
    {
        var marblesRemaining = FindObjectsOfType<Sim15Marble>().ToArray();
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
                CancelInvoke(nameof(SpawnObstacle));
                DisplayEndGamePanel(lastMarbleRenderer.material.color);
            }
        }
    }

    private void DisplayEndGamePanel(Color color)
    {
        endGamePanel.SetActive(true);

        var leaderboardIcon = Instantiate(leaderboardIconPrefab, new Vector3(0f, -0.2f, 0f), Quaternion.identity);
        leaderboardIcon.transform.SetParent(endGamePanel.transform);
        leaderboardIcon.transform.localScale = new Vector3(WinnerScaleFactor, WinnerScaleFactor);

        var iconRenderer = leaderboardIcon.GetComponent<Renderer>();
        iconRenderer.material.color = color;
    }

    private void SpawnStartingMarbles()
    {
        for (var i = 0; i < colors.Count; i++)
        {
            var spawnPos = startingMarbleSpawnPositions[i];
            var color = colors[i];
            var marble = SpawnMarble(spawnPos, color);
            var marbleScript = marble.GetComponent<Sim15Marble>();
            marbleScript.AssignId(i);
        }
    }

    private GameObject SpawnMarble(Vector3 spawnPos, Color color)
    {
        var marble = Instantiate(marblePrefab, spawnPos, Quaternion.identity);
        SetupMarble(marble, color);
        return marble;
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


    private void SpawnObstacle()
    {
        var randomAngle = UnityEngine.Random.Range(0f, 2f * Mathf.PI);

        var spawnX = ObstacleSpawnDistance * Mathf.Cos(randomAngle);
        var spawnY = ObstacleSpawnDistance * Mathf.Sin(randomAngle);
        var spawnPos = new Vector2(spawnX, spawnY);
        Instantiate(ObstacleBlockPrefab, spawnPos, Quaternion.identity);
    }
}
