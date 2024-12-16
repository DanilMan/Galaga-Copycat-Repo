using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class EnemyArray : MonoBehaviour
{
    public EnemySpawnManager EnemySpawner;
    public int gridX = 15;
    public int gridY = 5;
    private int enemyCount;
    public float gridSpacingOffSet = 0.75f;
    public int enemySpawnCount = 10;
    public float gridOriginQuadrantX = 2f;
    public float gridOriginQuadrantY = 4f;
    public float maxInterval = 2.5f;
    public float minInterval = 1f;
    [SerializeField] private PointSystem pointSystem;
    private Vector2 gridOrigin;
    private EnemySpawnManager[,] ESMs;
    private List<int[]> Pos = new List<int[]>();
    private List<int[]> Pops = new List<int[]>();
    private float spawnInterval = 1f;
    private float lastKill;
    private System.Random rnd = new System.Random();

    private void Start()
    {
        enemyCount = gridX * gridY;
        gridOrigin = new Vector2((gridX - 1) * gridSpacingOffSet / gridOriginQuadrantX, (gridY - 1) * gridSpacingOffSet / gridOriginQuadrantY); // get centered origin
        ESMs = new EnemySpawnManager[gridX, gridY];
        SpawnGrid();
        Invoke("GetSpawner", 1f);
        lastKill = Time.time;
    }

    private void SpawnGrid()
    {
        for (int x = 0; x < gridX; x++)
        {
            for (int y = 0; y < gridY; y++)
            {
                Vector3 spawnPosition = new Vector2(x * gridSpacingOffSet, y * gridSpacingOffSet) - gridOrigin;
                SpawnEnemyManager(spawnPosition, Quaternion.identity, x, y);
            }
        }
    }

    private void SpawnEnemyManager(Vector2 spawnPosition, Quaternion rotation, int x, int y)
    {
        EnemySpawnManager ESM = Instantiate(EnemySpawner, spawnPosition, rotation, transform);
        ESM.setXY(x, y);
        ESMs[x, y] = ESM;
        storeRandSpawns(x, y);
    }

    private void storeRandSpawns(int x, int y)
    {
        int[] cord = { x, y };
        int n = Pos.Count;
        if (n == 0)
        {
            Pos.Add(cord);
        }
        else
        {
            Pos.Add(cord);
            int k = rnd.Next(n + 1);
            int[] value = Pos[k];
            Pos[k] = Pos[n];
            Pos[n] = value;
        }
    }
    
    private void GetSpawner()
    {
        if (Pos.Count > (enemyCount - enemySpawnCount))
        {
            int[] spawner = Pos[0];
            ESMs[spawner[0], spawner[1]].Spawn();
            Pos.RemoveAt(0);

            Pops.Add(spawner);
        }

        float currInterval = (Time.time - lastKill);
        if (currInterval > maxInterval + 0.5f) { spawnInterval = maxInterval + 0.5f; }
        else { if (currInterval > minInterval) { spawnInterval = currInterval; } }

        Invoke("GetSpawner", spawnInterval);
    }

    public void decantSpawner(int x, int y)
    {
        float newInterval = (Time.time - lastKill)/2f;
        lastKill = Time.time;
        if (newInterval > maxInterval) { spawnInterval = maxInterval; }
        else if (newInterval < minInterval) { spawnInterval = minInterval; }
        else { spawnInterval = newInterval; }

        if (Pops.Count > 0)
        {
            int[] spawner = Pops[0];
            Pos.Add(spawner);
            Pops.RemoveAt(0);
        }
    }

    public void countPoints(uint points)
    {
        int increase = 1 + (Pops.Count/(enemySpawnCount/8)) * (1 + (int)Mathf.Round((Time.fixedTime / 60f)));
        points *= ((uint)increase);
        pointSystem.addPoints(points);
    }
}
