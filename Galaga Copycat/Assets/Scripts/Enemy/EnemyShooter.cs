using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private float randStartTimeBegin, randStartTimeEnd; // (3,6)
    [SerializeField] private float randShootTimeBegin, randShootTimeEnd; // (2,4)
    private SpriteRenderer spriteRenderer;
    private EnemyBehavior eb;

    public EnemyProjectileBehavior ProjectilePrefab;
    private GameObject Player;

    [SerializeField] private AudioClip pewTransient;
    
    private float timer;
    private void Start()
    {
        float randomTime = UnityEngine.Random.Range(randStartTimeBegin, randStartTimeEnd);
        spriteRenderer = GetComponent<SpriteRenderer>();
        eb = GetComponent<EnemyBehavior>();
        Player = eb.Player;
        eb.setExplosionPitchRange(0.8f, 1);
        Invoke("Shoot", randomTime);
    }

    private void Shoot()
    {
        if (Player == null) return;
        SoundFXManager.instance.PlaySoundFXClip(pewTransient, transform, transform, 1f, 0.8f, 0.1f, Random.Range(.9f, 1.1f), false, 0.5f, 9);
        Vector3 position = transform.position - new Vector3(0, spriteRenderer.bounds.size.y/2, 0);
        Vector3 targetDirection = Player.transform.position - position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0f);
        Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 180) * targetDirection;
        Quaternion rotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: rotatedVectorToTarget);
        EnemyProjectileBehavior projectile = Instantiate(ProjectilePrefab, position, rotation);
        projectile.Initialize(targetDirection, eb);

        float randomTime = UnityEngine.Random.Range(randShootTimeBegin, randShootTimeEnd);
        Invoke("Shoot", randomTime);
    }
}
