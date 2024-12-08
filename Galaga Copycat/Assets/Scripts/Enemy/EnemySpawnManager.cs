using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject[] Enemies;
    [SerializeField] private EnemyArray enemyArray;
    [SerializeField] private Vector3 spawnOffset;
    private GameObject Enemy;
    private GameObject clone;
    private EnemyArray Parent;
    private int x;
    private int y;
    private int Pos;
    System.Random rnd = new System.Random();

    public void setXY(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public void Start()
    {
        Parent = transform.parent.GetComponent<EnemyArray>();
        Pos = transform.position.x >= 0 ? 1 : -1;
    }

    public void shipDestroyed()
    {
        Parent.decantSpawner(x, y);
    }

    public void Spawn()
    {
        if(clone == null)
        {
            int randomIndex = rnd.Next(0, Enemies.Length);
            Enemy = Enemies[randomIndex];
            this.tag = Enemy.tag;
            Vector3 position = transform.position + new Vector3(spawnOffset.x * Pos, spawnOffset.y , spawnOffset.z);
            clone = Instantiate(Enemy, position, Quaternion.identity, transform);
        }
    }
}
