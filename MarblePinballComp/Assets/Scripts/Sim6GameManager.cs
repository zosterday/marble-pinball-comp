using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Sim6GameManager : MonoBehaviour
{
    private const float StartingMarbleSpawnBoxBound = 0.3f;

    private const float StartingMarbleSpawnBoxYOffset = 3.5f;

    private const float WinnerScaleFactor = 120f;

    public bool IsSimActive { get; private set; }

    private static Sim6GameManager instance;

    private bool isSimEnded = false;

    private Color winnerColor;

    [SerializeField]
    private GameObject leaderboardIconPrefab;

    [SerializeField]
    private GameObject endGamePanel;

    [SerializeField]
    private GameObject startingBox;

    [SerializeField]
    private GameObject DestroyableBlockPrefab;

    [SerializeField]
    private List<Color> colors;

    public List<Color> Colors
    {
        get
        {
            return colors;
        }
    }

    public static Sim6GameManager Instance
    {
        get
        {
            if (instance is null)
            {
                throw new NullReferenceException("Instance of Sim6GameManager is null");
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
        SetupScene();

        IsSimActive = true;

        Invoke(nameof(DestroyStartingBox), 1.5f);
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
    }

    public void EndGame(Color color)
    {
        isSimEnded = true;
        winnerColor = color;
    }

    private void DisplayEndGamePanel()
    {
        endGamePanel.SetActive(true);

        var leaderboardIcon = Instantiate(leaderboardIconPrefab, Vector3.zero, Quaternion.identity);
        leaderboardIcon.transform.SetParent(endGamePanel.transform);
        leaderboardIcon.transform.localScale = new Vector3(WinnerScaleFactor, WinnerScaleFactor);

        var iconRenderer = leaderboardIcon.GetComponent<Renderer>();
        iconRenderer.material.color = winnerColor;
    }

    private void DestroyStartingBox()
    {
        Destroy(startingBox);
    }

    private void SetupScene()
    {
        foreach (var color in colors)
        {
            var x = UnityEngine.Random.Range(-StartingMarbleSpawnBoxBound, StartingMarbleSpawnBoxBound);
            var y = StartingMarbleSpawnBoxYOffset + UnityEngine.Random.Range(-StartingMarbleSpawnBoxBound, StartingMarbleSpawnBoxBound);
            var spawnPos = new Vector3(x, y, 1f);
            Sim4SpawnManager.Instance.SpawnMarble(spawnPos, color);
        }
    }
}
