using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Sim12GameManager : MonoBehaviour
{
    private const float WinnerScaleFactor = 120f;

    [SerializeField]
    private GameObject leaderboardIconPrefab;

    [SerializeField]
    private GameObject endGamePanel;

    [SerializeField]
    private GameObject redLifeMarkers;

    [SerializeField]
    private GameObject blueLifeMarkers;

    [SerializeField]
    private GameObject greenLifeMarkers;

    [SerializeField]
    private GameObject yellowLifeMarkers;

    private List<Sim12DestroyableBlock> redBlocks;
    private List<Sim12DestroyableBlock> blueBlocks;
    private List<Sim12DestroyableBlock> greenBlocks;
    private List<Sim12DestroyableBlock> yellowBlocks;

    private static Sim12GameManager instance;

    private Color winnerColor;

    private bool isSimEnded = false;

    private int deadCount;

    public bool IsSimActive { get; private set; }

    public static Sim12GameManager Instance
    {
        get
        {
            if (instance is null)
            {
                throw new NullReferenceException("Instance of Sim12GameManager is null");
            }

            return instance;
        }
    }

    private void Awake()
    {
        IsSimActive = true;
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        redBlocks = redLifeMarkers.GetComponentsInChildren<Sim12DestroyableBlock>().ToList();
        blueBlocks = blueLifeMarkers.GetComponentsInChildren<Sim12DestroyableBlock>().ToList();
        greenBlocks = greenLifeMarkers.GetComponentsInChildren<Sim12DestroyableBlock>().ToList();
        yellowBlocks = yellowLifeMarkers.GetComponentsInChildren<Sim12DestroyableBlock>().ToList();
    }

    void Update()
    {
        if (!IsSimActive)
        {
            return;
        }

        if (deadCount == 3)
        {
            if (redBlocks.Count > 0)
            {
                EndGame("red");
            }
            if (blueBlocks.Count > 0)
            {
                EndGame("blue");
            }
            if (greenBlocks.Count > 0)
            {
                EndGame("green");
            }
            if (yellowBlocks.Count > 0)
            {
                EndGame("yellow");
            }
        }

        if (isSimEnded)
        {
            IsSimActive = false;
            Invoke(nameof(DisplayEndGamePanel), 0.5f);
        }
    }

    private void EndGame(string color)
    {
        isSimEnded = true;
        if (color == "red")
        {
            ColorUtility.TryParseHtmlString("#FF0000", out winnerColor);
        }
        if (color == "blue")
        {
            ColorUtility.TryParseHtmlString("#003DFF", out winnerColor);
        }
        if (color == "green")
        {
            ColorUtility.TryParseHtmlString("#39FF00", out winnerColor);
        }
        if (color == "yellow")
        {
            ColorUtility.TryParseHtmlString("#FFF500", out winnerColor);
        }
    }

    private void DisplayEndGamePanel()
    {
        endGamePanel.SetActive(true);

        var leaderboardIcon = Instantiate(leaderboardIconPrefab, Vector3.zero, Quaternion.identity);
        leaderboardIcon.transform.SetParent(endGamePanel.transform);
        leaderboardIcon.transform.position = new Vector3(0f, 0f, 1f);
        leaderboardIcon.transform.localScale = new Vector3(WinnerScaleFactor, WinnerScaleFactor);

        var iconRenderer = leaderboardIcon.GetComponent<Renderer>();
        iconRenderer.material.color = winnerColor;
    }

    public void TakeLife(string color)
    {
        if (color == "red")
        {
            var lastBlock = redBlocks[redBlocks.Count - 1];
            StartCoroutine(lastBlock.BreakBlock());
            redBlocks.Remove(lastBlock);
            if (redBlocks.Count == 0)
            {
                deadCount++;
            }
        }
        if (color == "blue")
        {
            var lastBlock = blueBlocks[blueBlocks.Count - 1];
            StartCoroutine(lastBlock.BreakBlock());
            blueBlocks.Remove(lastBlock);
            if (blueBlocks.Count == 0)
            {
                deadCount++;
            }
        }
        if (color == "green")
        {
            var lastBlock = greenBlocks[greenBlocks.Count - 1];
            StartCoroutine(lastBlock.BreakBlock());
            greenBlocks.Remove(lastBlock);
            if (greenBlocks.Count == 0)
            {
                deadCount++;
            }
        }
        if (color == "yellow")
        {
            var lastBlock = yellowBlocks[yellowBlocks.Count - 1];
            StartCoroutine(lastBlock.BreakBlock());
            yellowBlocks.Remove(lastBlock);
            if (yellowBlocks.Count == 0)
            {
                deadCount++;
            }
        }
    }
}
