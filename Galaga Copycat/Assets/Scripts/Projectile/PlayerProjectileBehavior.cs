using UnityEngine;

public class PlayerProjectileBehavior : MonoBehaviour
{
    
    public float speed = 5.0f;
    private Rigidbody2D rb;
    private Vector2 _direction;
    [SerializeField] private AudioClip pew;
    [SerializeField] private ParticleSystem collisionEffect;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _direction = new Vector2(0f, 1f);
    }

    void Start()
    {
        rb.linearVelocity = _direction * speed;
        SoundFXManager.instance.PlaySoundFXClip(pew, transform, transform, 0.4f, 1f, 0.1f, Random.Range(.6f, 1.4f), false, 0.5f, 6);
        Destroy(gameObject, speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ParticleSystem particleSystem = Instantiate(collisionEffect, transform.position, Quaternion.identity);
        Destroy(particleSystem.gameObject, 1f);
        Destroy(gameObject);
    }

}
