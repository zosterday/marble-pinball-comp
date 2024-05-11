using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Sim17GameManager : MonoBehaviour
{
    public bool IsSimActive { get; private set; }

    private static Sim17GameManager instance;

    private bool isSimEnded = false;

    [SerializeField]
    private GameObject leaderboardIconPrefab;

    [SerializeField]
    private GameObject endGamePanel;

    [SerializeField]
    private GameObject marble1;

    [SerializeField]
    private GameObject marble2;

    public static Sim17GameManager Instance
    {
        get
        {
            if (instance is null)
            {
                throw new NullReferenceException("Instance of Sim17GameManager is null");
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
    }

    public void EndGame()
    {
        isSimEnded = true;
    }

    private void DisplayEndGamePanel()
    {
        endGamePanel.SetActive(true);
    }
}
