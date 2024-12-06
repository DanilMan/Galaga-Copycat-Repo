using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private AudioClip engineHum;
    [SerializeField] float destroyDelay;

    private void Start()
    {
        SoundFXManager.instance.PlaySoundFXClip(engineHum, transform, transform, 0.10f, 1f, 0f, 1f, true);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject, destroyDelay);
    }
}
