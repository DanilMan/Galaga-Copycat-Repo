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

    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, Transform parent = null, float volume = 1f, float spacialBlend = 0f, float bufferTime = 0.1f)
    {
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity, parent);

        audioSource.clip = audioClip;

        audioSource.volume = volume;

        audioSource.spatialBlend = spacialBlend;

        audioSource.Play();

        float clipLength = audioSource.clip.length + bufferTime;

        Destroy(audioSource.gameObject, clipLength);
    }

    public void PlayRandomSoundFXClip(AudioClip[] audioClip, Transform spawnTransform, Transform parent = null, float volume = 1f, float spacialBlend = 0f, float bufferTime = 0.1f)
    {
        int rand = Random.Range(0, audioClip.Length);

        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity, parent);

        audioSource.clip = audioClip[rand];

        audioSource.volume = volume;

        audioSource.spatialBlend = spacialBlend;

        audioSource.Play();

        float clipLength = audioSource.clip.length + bufferTime;

        Destroy(audioSource.gameObject, clipLength);
    }
}
