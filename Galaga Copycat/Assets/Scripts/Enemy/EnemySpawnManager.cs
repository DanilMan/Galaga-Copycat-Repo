using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject[] Enemies;
    private GameObject Enemy;

    public void Awake()
    {
        System.Random rnd = new System.Random();
        int randomIndex = rnd.Next(0, Enemies.Length);
        Enemy = Enemies[randomIndex];
        this.tag = Enemy.tag;
    }

    public void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        GameObject clone = Instantiate(Enemy, transform.position, Quaternion.identity);
    }
}
