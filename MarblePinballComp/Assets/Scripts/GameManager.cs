using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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

    private float countdownTimer = 60f;

    private Dictionary<Color, int> scoresByColor = new();

    [SerializeField]
    public List<Color> colors;

    //[SerializeField]
    //private GameObject endGamePanel;

    //[SerializeField]
    //private TextMeshProUGUI countdownTimerText;

    private void Awake()
    {
        IsSimActive = false;
        instance = this;

        AddColorEntries();
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnManager.Instance.StartMarbleSpawn();
        IsSimActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsSimActive)
        {
            return;
        }

        if (countdownTimer <= 0f)
        {
            IsSimActive = false;
            EndGame();
            return;
        }

        countdownTimer -= Time.deltaTime;
        //countdownTimerText.text = countdownTimer.ToString("0.00");
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
    }

    private void EndGame()
    {
        SpawnManager.Instance.EndMarbleSpawn();

        //TODO: Make game wait for all the marbles to finish falling. Just stop spawning first

        //call method to determine winner and set correct fields
        //endGamePanel.SetActive(true);
    }
}
