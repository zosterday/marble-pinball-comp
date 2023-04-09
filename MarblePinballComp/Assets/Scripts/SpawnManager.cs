using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private const float XSpawnMin = -6f;

    private const float XSpawnMax = 6f;

    public static SpawnManager Instance
    {
        get
        {
            if (instance is null)
            {
                throw new System.InvalidOperationException("Instance of GameManager is null");
            }

            return instance;
        }
    }

    private static SpawnManager instance;

    private Vector3 firstSpawnPos = new Vector3(XSpawnMin, 5.25f, 1f);

    private float xSpawnOffset;

    private List<Color> colors;

    [SerializeField]
    private GameObject marblePrefab;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        colors = GameManager.Instance.Colors;
        xSpawnOffset = (XSpawnMax - XSpawnMin) / (colors.Count - 1);
    }

    public void StartMarbleSpawn()
    {
        InvokeRepeating(nameof(SpawnMarbles), 1f, 4f);
    }

    public void EndMarbleSpawn()
    {
        CancelInvoke(nameof(SpawnMarbles));
    }

    private void SpawnMarbles()
    {
        for (var i = 0; i < colors.Count; i++)
        {
            var spawnPos = new Vector3(firstSpawnPos.x + (xSpawnOffset * i), firstSpawnPos.y, firstSpawnPos.z);
            var marble = Instantiate(marblePrefab, spawnPos, Quaternion.identity);

            var color = colors[i];
            var renderer = marble.GetComponent<SpriteRenderer>();
            renderer.color = color;
            var trailRenderer = marble.GetComponent<TrailRenderer>();
            trailRenderer.startColor = color;
        }
    }
}



//TODO: Make marbles slightly bigger and make trail smaller or same size
//Fix the colors of the marbles and the trails 