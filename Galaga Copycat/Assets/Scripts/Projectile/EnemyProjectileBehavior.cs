using UnityEngine;

public class EnemyProjectileBehavior : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody2D rb;
    private Vector3 _direction;
    [SerializeField] private AudioClip pew;
    private EnemyBehavior parent;
    public LayerMask Layer;
    [SerializeField] private uint points = 1;

    public void Initialize(Vector3 direction, EnemyBehavior parent)
    {
        _direction = direction;
        this.parent = parent;
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        rb.linearVelocity = _direction.normalized * speed;
        SoundFXManager.instance.PlaySoundFXClip(pew, transform, transform, 0.15f, 1, 0.1f, Random.Range(2, 2.5f), false, 0.5f, 9);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Layer == (Layer.value & (1 << collision.gameObject.layer)))
        {
            parent.addChildrenPoints(points);
        }
        Destroy(gameObject);
    }
}
