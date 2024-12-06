using System;
using System.Threading;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private float2 randStartTimeRange;
    [SerializeField] private float2 randShootTimeRange;

    public ProjectileBehavior ProjectilePrefab;

    private float timer;
    private void Start()
    {
        float randomTime = UnityEngine.Random.Range(randStartTimeRange.x, randStartTimeRange.y);
        Invoke("Shoot", randomTime);
    }

    private void Shoot()
    {
        ProjectileBehavior projectile = Instantiate(ProjectilePrefab, transform.position, transform.rotation);
        float randomTime = UnityEngine.Random.Range(randShootTimeRange.x, randShootTimeRange.y);
        Invoke("Shoot", randomTime);
    }
}
