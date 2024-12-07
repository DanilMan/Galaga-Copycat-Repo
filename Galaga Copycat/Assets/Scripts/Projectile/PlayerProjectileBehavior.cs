using UnityEngine;

public class PlayerProjectileBehavior : MonoBehaviour
{
    
    public float speed = 5.0f;
    private Rigidbody2D rb;
    private Vector2 _direction;
    [SerializeField] private AudioClip laser;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _direction = new Vector2(0f, 1f);
    }

    void Start()
    {
        rb.linearVelocity = _direction * speed;
        SoundFXManager.instance.PlaySoundFXClip(laser, transform, transform, 1f, 1f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

}
