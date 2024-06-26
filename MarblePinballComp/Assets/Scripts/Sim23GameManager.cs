using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Sim23GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject racer1;

    [SerializeField]
    private GameObject racer2;

    [SerializeField]
    private GameObject racer3;

    [SerializeField]
    private ParticleSystem collisionParticle;

    [SerializeField]
    private GameObject endGamePanel;

    [SerializeField]
    private TextMeshProUGUI winnerText;

    private static Sim23GameManager instance;

    public static Sim23GameManager Instance
    {
        get
        {
            if (instance is null)
            {
                throw new NullReferenceException("Instance of Sim23GameManager is null");
            }

            return instance;
        }
    }

    public bool IsEnded { get; private set; }

    private void Awake()
    {
        instance = this;
        Invoke(nameof(StartSim), 2f);
    }

    // Start is called before the first frame update
    void StartSim()
    {
        racer1.SetActive(true);
        racer2.SetActive(true);
        racer3.SetActive(true);
    }

    public void PlayCollisionParticle(Vector3 position)
    {
        collisionParticle.transform.position = position;
        collisionParticle.Play();
    }

    public void EndRace(string winnerName)
    {
        IsEnded = true;
        winnerText.text = $"{winnerName} wins!";
        endGamePanel.SetActive(true);
    }
}
