using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private AudioClip engineHum;
    [SerializeField] float destroyDelay;
    [NonSerialized] public GameObject Player;
    private EnemySpawnManager Parent;

    Vector3 targetDirection;
    private Vector3 center;
    [SerializeField] private float journeyTime = 2.0f;
    private Vector3 storedTransform;
    private Vector3 storedParentTransform;
    private Quaternion storedParentRotation;
    private Vector3 enemyCenter;
    private Vector3 parentCenter;

    [SerializeField] private float lerpSpeed = 2f;

    [SerializeField] private AudioClip explosion;
    [SerializeField] private AudioClip explosionTransient;
    [SerializeField] private AudioClip Engine;
    [SerializeField] private ParticleSystem explodeSystem;
    [NonSerialized] public AudioSource engineSource;

    [SerializeField] private uint pointValue;

    public LayerMask Layer;

    private float[] explosionPitchRange = {0.9f, 1.1f};

    private bool isQuitting = false;
    private bool isExploding = true;

    private void Start()
    {
        Parent = transform.parent.GetComponent<EnemySpawnManager>();
        Player = GameObject.Find("Ship");
        engineSource = SoundFXManager.instance.PlaySoundFXClip(Engine, transform, transform, 0.6f, 1f, 0.1f, 1, true, 0.5f, 5);

        storedTransform = transform.position;
        storedParentTransform = Parent.transform.position;
        storedParentRotation = Parent.transform.rotation;      

        center = (storedParentTransform + storedTransform) * 0.5f;
        center += new Vector3(0, 1, 0);

        enemyCenter = storedTransform - center;
        parentCenter = storedParentTransform - center;

        StartCoroutine(spawnInMotion());
    }

    public void setExplosionPitchRange(float first, float second)
    {
        this.explosionPitchRange[0] = first;
        this.explosionPitchRange[1] = second;
    }

    private IEnumerator spawnInMotion()
    {
        float fracComplete = 0f;
        float time = 0f;
        while (fracComplete < 1f)
        {
            time += Time.deltaTime;
            fracComplete = time / journeyTime;

            Vector3 slerp = Vector3.Slerp(enemyCenter, parentCenter, fracComplete);

            Vector3 oldTransform = transform.position;

            transform.position = slerp;
            transform.position += center;

            if (transform.position != oldTransform)
            {
                targetDirection = transform.position - oldTransform;
                Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 180) * targetDirection;
                transform.rotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: rotatedVectorToTarget);
            }
            yield return null;
        }
        StartCoroutine(correctRotation());
    }

    private IEnumerator correctRotation()
    {
        float time = 0f;
        Quaternion startRotation = transform.rotation;
        while (time < 1f)
        {
            transform.rotation = Quaternion.Slerp(startRotation, storedParentRotation, time);
            time += Time.deltaTime * lerpSpeed;
            yield return null;
        }
        transform.rotation = storedParentRotation;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Parent.shipDestroyed();
        if (Layer != (Layer.value & (1 << collision.gameObject.layer)))
        {
            SoundFXManager.instance.PlaySoundFXClip(explosionTransient, transform, transform, 1f, 1f, 0.1f, UnityEngine.Random.Range(2, 2.5f), false, 0.5f, 6);
        }
        else
        {
            isExploding = false;
        }
        Destroy(gameObject, destroyDelay);
    }

    void OnApplicationQuit()
    {
        isQuitting = true;
    }

    private void OnDestroy()
    {
        if (!isQuitting && isExploding && Player != null)
        {
            Parent.shipPoints(pointValue);
            SoundFXManager.instance.PlaySoundFXClip(explosion, transform, null, 1f, 0.95f, 0.1f, UnityEngine.Random.Range(explosionPitchRange[0], explosionPitchRange[1]), false, 0.5f, 10);
            Instantiate(explodeSystem, transform.position, Quaternion.identity);
        }
    }

    public void addChildrenPoints(uint points)
    {
        Parent.shipPoints(points);
    }

    public void addPointValue(uint points)
    {
        pointValue += points;
    }
}
