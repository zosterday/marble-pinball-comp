using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const float WinnerScaleFactor = 120f;

    public bool IsSimActive { get; private set; }

    public List<Color> Colors
    {
        get
        {
            return colors;
        }
    }

    public static GameManager Instance
    {
        get
        {
            if (instance is null)
            {
                throw new NullReferenceException("Instance of GameManager is null");
            }

            return instance;
        }
    }

    private static GameManager instance;

    private readonly Dictionary<Color, int> scoresByColor = new();

    private readonly Dictionary<Color, GameObject> leaderboardIconsByColor = new();

    private readonly List<GameObject> leaderboardIcons = new();

    private readonly Vector3 screenOffset = new Vector3(70f, 100f, 0f);

    private readonly Vector3 placementYOffset = new Vector3(0f, -50f, 0f);

    private float countdownTimer = 25f;

    private bool isSimEnded = false;

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
    
    private void Awake()
    {
        IsSimActive = false;
        instance = this;

        AddColorEntries();
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (var color in colors)
        {
            var leaderboardIcon = Instantiate(leaderboardIconPrefab, Vector3.zero, Quaternion.identity);

            leaderboardIcon.transform.SetParent(leaderboardPanel.transform);

            var iconRenderer = leaderboardIcon.GetComponent<Renderer>();
            iconRenderer.material.color = color;

            leaderboardIcons.Add(leaderboardIcon);
            leaderboardIconsByColor.Add(color, leaderboardIcon);
        }

        SpawnManager.Instance.StartMarbleSpawn();
        UpdateLeaderboard();
        IsSimActive = true;
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
            var marblesRemaining = FindObjectsOfType<Marble>().Length;
            if (marblesRemaining > 0)
            {
                return;
            }

            IsSimActive = false;
            DisplayEndGamePanel();
        }



        if (countdownTimer <= 0f)
        {
            EndGame();
            return;
        }
        
        countdownTimer -= Time.deltaTime;
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
        SpawnManager.Instance.EndMarbleSpawn();
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
}
