using System;
using System.Collections;
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

    [SerializeField] private AudioClip explosion;
    [SerializeField] private AudioClip pewTransient;
    [SerializeField] private AudioClip Engine;
    [SerializeField] private ParticleSystem explodeSystem;
    private AudioSource engineSource;

    public LayerMask enemyLayer;

    public PlayerProjectileBehavior ProjectilePrefab;
    public Transform LaunchOffset;
    [SerializeField] private SoundMixerManager SMM;
    public GameOverController gameOver;
    [SerializeField] private float deadWait;
    private int shootSwitch = 1;
    [SerializeField] float shootTime;
    private Coroutine shootCoroutine;

    private bool isQuitting = false;
    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        engineSource = SoundFXManager.instance.PlaySoundFXClip(Engine, transform, transform, 0.55f, 0f, 0.1f, 1, true);
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

        engineSource.pitch = ((Mathf.Abs(_smoothedMovementInput.x) + Mathf.Abs(_smoothedMovementInput.y)) / speed) + .80f;
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        if (!PauseMenu.GameIsPaused)
        {
            if (context.canceled)
            {
                StopCoroutine(shootCoroutine);
            }
            else if (context.started)
            {
                shootCoroutine = StartCoroutine(loopShoot());
            }
        }
    }

    private IEnumerator loopShoot()
    {
        shootSwitch *= -1;
        Vector3 LaunchOffsetSwitch = LaunchOffset.position + new Vector3(0.04f * shootSwitch, 0f, 0f);
        PlayerProjectileBehavior projectile = Instantiate(ProjectilePrefab, LaunchOffsetSwitch, transform.rotation);
        SoundFXManager.instance.PlaySoundFXClip(pewTransient, transform, transform, 0.5f, 0f, 0.1f, UnityEngine.Random.Range(0.8f, 1f));
        yield return new WaitForSeconds(shootTime);
        shootCoroutine = StartCoroutine(loopShoot());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(enemyLayer == (enemyLayer.value & (1 << collision.gameObject.layer))){
            AudioListener m_audioListener = GetComponent<AudioListener>();
            GameObject listener = new GameObject("Listener");
            listener.transform.position = transform.position;
            Destroy(m_audioListener);
            listener.AddComponent<AudioListener>();
            SoundFXManager.instance.PlaySoundFXClip(explosion, transform, null, 0.85f, 0f, 0.1f, UnityEngine.Random.Range(0.9f, 1.1f));
            Instantiate(explodeSystem, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
            gameOver.lerpMuteMaster();
            Destroy(gameObject, deadWait);
        }
    }
    void OnApplicationQuit()
    {
        isQuitting = true;
    }

    private void OnDestroy()
    {
        if (!isQuitting)
        {
            gameOver.playerDied();
        }
    }
}
