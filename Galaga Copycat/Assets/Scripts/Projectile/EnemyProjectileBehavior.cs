using UnityEngine;

public class EnemyProjectileBehavior : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody2D rb;
    private Vector3 _direction;
    [SerializeField] private AudioClip laser;

    public void Initialize(Vector3 direction)
    {
        _direction = direction;
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        rb.linearVelocity = _direction.normalized * speed;
        SoundFXManager.instance.PlaySoundFXClip(laser, transform, transform, 1f, 1f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
