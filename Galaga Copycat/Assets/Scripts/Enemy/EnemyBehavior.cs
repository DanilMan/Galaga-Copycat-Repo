using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private AudioClip engineHum;
    [SerializeField] float destroyDelay;
    private EnemySpawnManager Parent;

    private void Start()
    {
        Parent = transform.parent.GetComponent<EnemySpawnManager>();
        SoundFXManager.instance.PlaySoundFXClip(engineHum, transform, transform, 0.10f, 1f, 0f, 1f, true);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Parent.shipDestroyed();
        Destroy(gameObject, destroyDelay);
    }
}
