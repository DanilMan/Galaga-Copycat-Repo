using Unity.Properties;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D m_Rigidbody2D;
    private Vector2 _input;
    private Vector2 _direction;
    private Vector2 _smoothedMovementInput;
    private Vector2 m_smoothedMovementVelocity;
    [Range(0.1f, 20)][SerializeField] private float speed;
    [Range(0.1f, 1)][SerializeField] private float smoothTime;

    public LayerMask enemyLayer;

    public PlayerProjectileBehavior ProjectilePrefab;
    public Transform LaunchOffset;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Move(InputAction.CallbackContext context)
    {
        _input = context.ReadValue<Vector2>();
        _direction = new Vector2(_input.x, _input.y);
    }

    private void FixedUpdate()
    {
        _smoothedMovementInput = Vector2.SmoothDamp(_smoothedMovementInput, _direction, ref m_smoothedMovementVelocity, smoothTime);
        m_Rigidbody2D.linearVelocity = _smoothedMovementInput * speed;
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.performed && !PauseMenu.GameIsPaused)
        {
            PlayerProjectileBehavior projectile = Instantiate(ProjectilePrefab, LaunchOffset.position, transform.rotation);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(enemyLayer == (enemyLayer.value & (1 << collision.gameObject.layer))){
            AudioListener m_audioListener = GetComponent<AudioListener>();
            GameObject listener = new GameObject("Listener");
            listener.transform.position = transform.position;
            Destroy(m_audioListener);
            listener.AddComponent<AudioListener>();
            Destroy(gameObject);
        }
        //if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyProjectile")
        //{
        //    AudioListener m_audioListener= GetComponent<AudioListener>();
        //    GameObject listener = new GameObject("Listener");
        //    listener.transform.position = transform.position;
        //    Destroy(m_audioListener);
        //    listener.AddComponent<AudioListener>();
        //    Destroy(gameObject);
        //}
    }
}
