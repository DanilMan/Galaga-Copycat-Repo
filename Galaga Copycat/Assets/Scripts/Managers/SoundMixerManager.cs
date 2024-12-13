using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider Master;
    [SerializeField] private Slider FX;
    [SerializeField] private Slider Music;
    [SerializeField] private float lerpSpeed = 1f;

    private void Start()
    {
        SetMasterVolume(Master.value);
        SetFXVolume(FX.value);
        SetMusicVolume(Music.value);
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 40); // *20 (min .0001)
    }

    public void SetFXVolume(float volume)
    {
        audioMixer.SetFloat("SoundFXVolume", Mathf.Log10(volume) * 40); // *20 (min .0001)
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 40); // *20 (min .0001)
    }

    public void setGameEnd()
    {
        StartCoroutine(LerpMuteMaster());
    }

    private IEnumerator LerpMuteMaster()
    {
        float time = 0f;
        float volume = 0f;
        audioMixer.GetFloat("MasterVolume", out volume);
        float initialVolume = volume;
        while (volume > -80f)
        {
            audioMixer.GetFloat("MasterVolume", out volume);
            volume = Mathf.Lerp(initialVolume, -80, time);
            time += Time.deltaTime * lerpSpeed;
            audioMixer.SetFloat("MasterVolume", volume);
            yield return null;
        }
    }
}
