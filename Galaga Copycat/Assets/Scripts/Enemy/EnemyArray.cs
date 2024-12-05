using UnityEngine;

public class EnemyArray : MonoBehaviour
{
    public GameObject[] enemies;
    public int gridX;
    public int gridY;
    public float gridSpacingOffSet = 1f;
    public float gridOriginQuadrantX = 2f;
    public float gridOriginQuadrantY = 4f;
    private Vector2 gridOrigin;

    private void Start()
    {
        gridOrigin = new Vector2((gridX - 1) * gridSpacingOffSet / gridOriginQuadrantX, (gridY - 1) * gridSpacingOffSet / gridOriginQuadrantY); // get centered origin
        SpawnGrid();
    }

    private void SpawnGrid()
    {
        for (int x = 0; x < gridX; x++)
        {
            for (int y = 0; y < gridY; y++)
            {
                Vector3 spawnPosition = new Vector2(x * gridSpacingOffSet, y * gridSpacingOffSet) - gridOrigin;
                PickAndSpawn(spawnPosition, Quaternion.identity);
            }
        }
    }

    private void PickAndSpawn(Vector3 spawnPosition, Quaternion rotation)
    {
        int randomIndex = Random.Range(0, enemies.Length);
        GameObject clone = Instantiate(enemies[randomIndex], spawnPosition, rotation);
    }
}
