using System.Threading;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private AudioClip engineHum;
    [SerializeField] float destroyDelay;
    private EnemySpawnManager Parent;
    private Rigidbody2D rb;

    Vector3 targetDirection;
    private bool Targeted = false;
    private bool finished = false;
    private Vector3 center;
    [SerializeField] private float journeyTime = 2.0f;
    private float startTime;
    private Vector3 storedTransform;
    private Vector3 storedPlayerTransform;
    private Quaternion storedParentRotation;
    private Vector3 enemyCenter;
    private Vector3 playerCenter;

    [SerializeField] private float lerpSpeed = 0.01f;
    private float timeCount = 0.0f;

    private void Start()
    {
        Parent = transform.parent.GetComponent<EnemySpawnManager>();
        rb = GetComponent<Rigidbody2D>();
        //SoundFXManager.instance.PlaySoundFXClip(engineHum, transform, transform, 0.10f, 1f, 0f, 1f, true);

        storedTransform = transform.position;
        storedPlayerTransform = Parent.transform.position;
        storedParentRotation = Parent.transform.rotation;      
        startTime = Time.time;

        center = (storedPlayerTransform + storedTransform) * 0.5f;
        center += new Vector3(0, 1, 0);

        enemyCenter = storedTransform - center;
        playerCenter = storedPlayerTransform - center;

        Targeted = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Parent.shipDestroyed();
        Destroy(gameObject, destroyDelay);
    }

    private void Update()
    {
        if (Targeted)
        {
            Targeted = spawnInMotion() <= 1f;
            finished = !Targeted;
        }
        if (finished)
        {
            correctRotation();
        }
    }

    private float spawnInMotion()
    {
        float fracComplete = (Time.time - startTime) / journeyTime;

        Vector3 slerp = Vector3.Slerp(enemyCenter, playerCenter, fracComplete);

        Vector3 oldTransform = transform.position;

        transform.position = slerp;
        transform.position += center;

        targetDirection = transform.position - oldTransform;
        Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 180) * targetDirection;
        transform.rotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: rotatedVectorToTarget);
        return fracComplete;
    }

    private void correctRotation()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, storedParentRotation, timeCount * lerpSpeed);
        timeCount = timeCount + Time.deltaTime;
        if (transform.rotation == storedParentRotation)
        {
            finished = false;
        }
    }
}
