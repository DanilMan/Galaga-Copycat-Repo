using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyRammer : MonoBehaviour
{
    [SerializeField] private float randStartTimeBegin, randStartTimeEnd; // (3,9)
    [SerializeField] private float speed;
    private Rigidbody2D rb;
    private EnemyBehavior eb;
    private bool Targeted = false;
    private bool Engage = false;

    Vector3 targetDirection;
    Quaternion rotation;
    private GameObject Player;

    [SerializeField]private float lerpSpeed = 0.01f;
    private float timeCount = 0.0f;

    [SerializeField] private AudioClip ramTransient;

    private void Start()
    {
        float randomTime = UnityEngine.Random.Range(randStartTimeBegin, randStartTimeEnd);
        rb = GetComponent<Rigidbody2D>();
        eb = GetComponent<EnemyBehavior>();
        Player = GameObject.Find("Ship");
        Invoke("Ram", randomTime);
    }
    private void Ram()
    {
        if (Player == null) return;
        targetDirection = Player.transform.position - transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0f);
        Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 180) * targetDirection;
        rotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: rotatedVectorToTarget);
        SoundFXManager.instance.PlaySoundFXClip(ramTransient, transform, transform, 1f, 0.9f, 0.1f, Random.Range(.9f, 1.1f), false, 0.5f, 12);
        Targeted = true;
    }

    private void Update()
    {
        if (Targeted)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, timeCount * lerpSpeed);
            timeCount = timeCount + Time.deltaTime;
            if (transform.rotation == rotation)
            {
                Targeted = false;
                Engage = true;
            }
        }
        if (Engage)
        {
            rb.linearVelocity = targetDirection * speed;
            eb.engineSource.pitch = 2f;
        }
    }
}
