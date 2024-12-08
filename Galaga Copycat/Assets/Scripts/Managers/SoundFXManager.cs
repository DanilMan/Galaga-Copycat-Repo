using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance;

    [SerializeField] private AudioSource soundFXObject;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public AudioSource PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, Transform parent = null, float volume = 1f, float spacialBlend = 0f, float bufferTime = 0.1f, float pitch = 1f, bool loop = false, float minDistance = 0.5f, float maxDistance = 12)
    {
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity, parent);

        audioSource.clip = audioClip;

        if (audioSource.clip.ambisonic == true)
        {
            audioSource.spatialize = false;
        }

        audioSource.volume = volume;

        audioSource.spatialBlend = spacialBlend;

        audioSource.pitch = pitch;

        audioSource.loop = loop;

        audioSource.minDistance = minDistance;
        
        audioSource.maxDistance = maxDistance;

        audioSource.Play();

        if (!audioSource.loop)
        {
            float clipLength = audioSource.clip.length + bufferTime;

            Destroy(audioSource.gameObject, clipLength);
        }
        return audioSource;
    }

    public AudioSource PlayRandomSoundFXClip(AudioClip[] audioClip, Transform spawnTransform, Transform parent = null, float volume = 1f, float spacialBlend = 0f, float bufferTime = 0.1f, float pitch = 1f, bool loop = false, float minDistance = 0.5f, float maxDistance = 12)
    {
        int rand = Random.Range(0, audioClip.Length);

        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity, parent);

        audioSource.clip = audioClip[rand];

        if (audioSource.clip.ambisonic == true)
        {
            audioSource.spatialize = false;
        }

        audioSource.volume = volume;

        audioSource.spatialBlend = spacialBlend;

        audioSource.pitch = pitch;

        audioSource.loop = loop;

        audioSource.minDistance = minDistance;

        audioSource.maxDistance = maxDistance;

        audioSource.Play();

        if (!audioSource.loop)
        {
            float clipLength = audioSource.clip.length + bufferTime;

            Destroy(audioSource.gameObject, clipLength);
        }
        return audioSource;
    }
}
