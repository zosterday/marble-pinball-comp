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
        InvokeRepeating(nameof(SpawnMarbles), 1f, 2f);
    }

    public void EndMarbleSpawn()
    {
        CancelInvoke(nameof(SpawnMarbles));
    }

    private void SpawnMarbles()
    {
        var colorList = new List<Color>(colors);
        var offsetMultiplier = 0;
        while (colorList.Count > 0)
        {
            var colorIndex = Random.Range(0, colorList.Count);

            var spawnPos = new Vector3(firstSpawnPos.x + (xSpawnOffset * offsetMultiplier), firstSpawnPos.y, firstSpawnPos.z);
            var marble = Instantiate(marblePrefab, spawnPos, Quaternion.identity);

            var color = colorList[colorIndex];
            var renderer = marble.GetComponent<Renderer>();
            renderer.material.color = color;
            var trailRenderer = marble.GetComponent<TrailRenderer>();
            trailRenderer.startColor = color;
            var endColor = color;
            endColor.a = 0f;
            trailRenderer.endColor = endColor;

            colorList.RemoveAt(colorIndex);
            offsetMultiplier++;
        }
    }
}



//TODO: Make marbles slightly bigger and make trail smaller or same size
