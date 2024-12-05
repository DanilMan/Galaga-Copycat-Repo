using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider Master;
    [SerializeField] private Slider FX;
    [SerializeField] private Slider Music;

    private void Start()
    {
        SetMasterVolume(Master.value);
        SetFXVolume(FX.value);
        SetMusicVolume(Music.value);
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MaserVolume", Mathf.Log10(volume) * 40); // *20 (min .0001)
    }

    public void SetFXVolume(float volume)
    {
        audioMixer.SetFloat("SoundFXVolume", Mathf.Log10(volume) * 40); // *20 (min .0001)
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 40); // *20 (min .0001)
    }
}
