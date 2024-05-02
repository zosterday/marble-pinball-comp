using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Sim14GameManager : MonoBehaviour
{
    private const float DestroyableBlockSpawnRadius = 1.9f;

    private const float WinnerScaleFactor = 120f;

    public bool IsSimActive { get; private set; }

    private static Sim14GameManager instance;

    private readonly Dictionary<Color, int> scoresByColor = new();

    private readonly Dictionary<Color, GameObject> leaderboardIconsByColor = new();

    private readonly List<GameObject> leaderboardIcons = new();

    private readonly List<Vector3> startingMarbleSpawnPositions = new()
    {
        new (-1.35f, 1f, 0f),
        new (0f, 1f, 0f),
        new (1.35f, 1f, 0f),
        new (-0.675f, 1.5f, 0f),
        new (0.675f, 1.5f, 0f)
    };

    private readonly Vector3 screenOffset = new Vector3(70f, 100f, 0f);

    private readonly Vector3 placementYOffset = new Vector3(0f, -50f, 0f);

    private float countdownTimer = 25f;

    private bool isSimEnded = false;

    [SerializeField]
    private GameObject DestroyableBlockPrefab;

    [SerializeField]
    private List<Color> colors;

    [SerializeField]
    private GameObject leaderboardIconPrefab;

    [SerializeField]
    private GameObject endGamePanel;

    [SerializeField]
    private GameObject leaderboardPanel;

    [SerializeField]
    private List<TextMeshProUGUI> leaderboardTexts;

    [SerializeField]
    private TextMeshProUGUI countdownTimerText;

    [SerializeField]
    private GameObject marblePrefab;

    public List<Color> Colors
    {
        get
        {
            return colors;
        }
    }

    public static Sim14GameManager Instance
    {
        get
        {
            if (instance is null)
            {
                throw new NullReferenceException("Instance of Sim14GameManager is null");
            }

            return instance;
        }
    }

    public int BlockCount { get; set; }

    private void Awake()
    {
        IsSimActive = false;
        instance = this;

        AddColorEntries();
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnStartingMarbles();
        foreach (var color in colors)
        {
            var leaderboardIcon = Instantiate(leaderboardIconPrefab, Vector3.zero, Quaternion.identity);

            leaderboardIcon.transform.SetParent(leaderboardPanel.transform);

            var iconRenderer = leaderboardIcon.GetComponent<Renderer>();
            iconRenderer.material.color = color;

            leaderboardIcons.Add(leaderboardIcon);
            leaderboardIconsByColor.Add(color, leaderboardIcon);
        }

        UpdateLeaderboard();
        IsSimActive = true;

        InvokeRepeating(nameof(SpawnDestroyableBlock), 2.5f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsSimActive)
        {
            return;
        }

        if (isSimEnded)
        {
            IsSimActive = false;
            DisplayEndGamePanel();
        }

        if (countdownTimer <= 0f && BlockCount >= 0)
        {
            CancelInvoke(nameof(SpawnDestroyableBlock));
        }

        if (countdownTimer <= 0f && BlockCount <= 0)
        {
            EndGame();
            return;
        }

        countdownTimer -= Time.deltaTime;
        if (countdownTimer <= 0f)
        {
            countdownTimer = 0f;
        }
        countdownTimerText.text = countdownTimer.ToString("0.00");
    }

    private void AddColorEntries()
    {
        foreach (var color in colors)
        {
            scoresByColor.Add(color, 0);
        }
    }

    public void UpdateScore(Color color, int pointsToAdd)
    {
        if (!scoresByColor.ContainsKey(color))
        {
            throw new InvalidOperationException($"Color: {color} not in color score dictionary.");
        }

        scoresByColor[color] += pointsToAdd;

        UpdateLeaderboard();
    }

    private void UpdateLeaderboard()
    {
        var colorsWithScores = scoresByColor
            .OrderByDescending(kvp => kvp.Value)
            .Take(5)
            .ToArray();

        leaderboardTexts[0].text = $"1st: ";
        leaderboardTexts[1].text = $"2nd: ";
        leaderboardTexts[2].text = $"3rd: ";
        leaderboardTexts[3].text = $"4th: ";
        leaderboardTexts[4].text = $"5th: ";

        foreach (var iconObj in leaderboardIcons)
        {
            iconObj.SetActive(false);
        }

        for (var i = 0; i < 5; i++)
        {
            var icon = leaderboardIconsByColor[colorsWithScores[i].Key];
            leaderboardTexts[i].text += colorsWithScores[i].Value;
            SetLeaderboardPosition(icon, i);
        }
    }

    private void SetLeaderboardPosition(GameObject leaderboardIcon, int placement)
    {
        leaderboardIcon.transform.localPosition = screenOffset + placementYOffset * placement;

        leaderboardIcon.gameObject.SetActive(true);
    }

    private void EndGame()
    {
        isSimEnded = true;
    }

    private void DisplayEndGamePanel()
    {
        var winner = scoresByColor
            .OrderByDescending(kvp => kvp.Value)
            .Take(1)
            .ToArray()[0];

        endGamePanel.SetActive(true);

        var leaderboardIcon = Instantiate(leaderboardIconPrefab, Vector3.zero, Quaternion.identity);
        leaderboardIcon.transform.SetParent(endGamePanel.transform);
        leaderboardIcon.transform.localScale = new Vector3(WinnerScaleFactor, WinnerScaleFactor);

        var iconRenderer = leaderboardIcon.GetComponent<Renderer>();
        iconRenderer.material.color = winner.Key;
    }

    private void SpawnStartingMarbles()
    {
        for (var i = 0; i < colors.Count; i++)
        {
            var spawnPos = startingMarbleSpawnPositions[i];
            var color = colors[i];
            SpawnMarble(spawnPos, color);
        }
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


    private void SpawnDestroyableBlock()
    {
        if (BlockCount >= 5)
        {
            return;
        }

        var randomDistance = UnityEngine.Random.Range(0f, DestroyableBlockSpawnRadius);
        var randomAngle = UnityEngine.Random.Range(0f, 2f * Mathf.PI);

        var spawnX = randomDistance * Mathf.Cos(randomAngle);
        var spawnY = randomDistance * Mathf.Sin(randomAngle);
        var spawnPos = new Vector2(spawnX, spawnY);
        Instantiate(DestroyableBlockPrefab, spawnPos, Quaternion.identity);
        BlockCount++;
    }
}
