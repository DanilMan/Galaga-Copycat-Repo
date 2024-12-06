using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    [SerializeField] private AudioSource musicObject;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlayMusicClip(AudioClip audioClip, Transform spawnTransform, Transform parent = null, float volume = 1f, float spacialBlend = 0f, float bufferTime = 0.1f, bool loop = false, float minDistance = 0.5f, float maxDistance = 12)
    {
        AudioSource audioSource = Instantiate(musicObject, spawnTransform.position, Quaternion.identity, parent);

        audioSource.clip = audioClip;

        audioSource.volume = volume;

        audioSource.spatialBlend = spacialBlend;

        audioSource.loop = loop;

        audioSource.minDistance = minDistance;

        audioSource.maxDistance = maxDistance;

        audioSource.Play();

        float clipLength = audioSource.clip.length + bufferTime;

        Destroy(audioSource.gameObject, clipLength);
    }

    public void PlayRandomMusicClip(AudioClip[] audioClip, Transform spawnTransform, Transform parent = null, float volume = 1f, float spacialBlend = 0f, float bufferTime = 0.1f, bool loop = false, float minDistance = 0.5f, float maxDistance = 12)
    {
        int rand = Random.Range(0, audioClip.Length);

        AudioSource audioSource = Instantiate(musicObject, spawnTransform.position, Quaternion.identity, parent);

        audioSource.clip = audioClip[rand];

        audioSource.volume = volume;

        audioSource.spatialBlend = spacialBlend;

        audioSource.loop = loop;

        audioSource.minDistance = minDistance;

        audioSource.maxDistance = maxDistance;

        audioSource.Play();

        float clipLength = audioSource.clip.length + bufferTime;

        Destroy(audioSource.gameObject, clipLength);
    }
}
