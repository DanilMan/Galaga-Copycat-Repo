using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyRammer : MonoBehaviour
{
    [SerializeField] private float randStartTimeBegin, randStartTimeEnd; // (3,9)
    [SerializeField] private float speed = 2.5f;
    [SerializeField] private uint ramPoints;
    private Rigidbody2D rb;
    private EnemyBehavior eb;

    Vector3 targetDirection;
    Quaternion rotation;
    private GameObject Player;

    [SerializeField]private float lerpSpeed = 1f;

    [SerializeField] private AudioClip ramTransient;

    private void Start()
    {
        float randomTime = UnityEngine.Random.Range(randStartTimeBegin, randStartTimeEnd);
        rb = GetComponent<Rigidbody2D>();
        eb = GetComponent<EnemyBehavior>();
        eb.setExplosionPitchRange(0.5f, 0.7f);
        Player = eb.Player;
        Invoke("Ram", randomTime);
    }
    private void Ram()
    {
        if (Player == null) return;
        targetDirection = Player.transform.position - transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0f);
        Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 180) * targetDirection;
        rotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: rotatedVectorToTarget);
        SoundFXManager.instance.PlaySoundFXClip(ramTransient, transform, transform, 1f, 0.6f, 0.1f, Random.Range(.9f, 1.1f), false, 0.5f, 12);

        StartCoroutine(rammingSpeed());
    }

    private IEnumerator rammingSpeed()
    {
        float time = 0f;
        Quaternion startRotation = transform.rotation;
        while (time < 1f)
        {
            transform.rotation = Quaternion.Slerp(startRotation, rotation, time);
            time += Time.deltaTime * lerpSpeed;
            yield return null;
        }
        rb.linearVelocity = targetDirection.normalized * speed;
        eb.engineSource.pitch = 2f;
        eb.addPointValue(ramPoints);
    }
}
