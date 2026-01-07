using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer audioMixer;
    [Header("Sliders")]
    [SerializeField] private Slider masterAudio;
    [SerializeField] private Slider musicAudio;
    [SerializeField] private Slider sfxAudio;
    private void Start()
    {
        if(PlayerPrefs.HasKey("Music") && PlayerPrefs.HasKey("SFX") && PlayerPrefs.HasKey("Master"))
        {
            LoadVolume();
        }
        else
        {
            masterAudio.value = 0.75f;
            musicAudio.value = 0.75f;
            sfxAudio.value = 0.75f;
            SetMasterVolume();
            SetMusicVolume();
            SetSFXVolume();
        }
    }
    public void SetMusicVolume()
    {
        float volume = musicAudio.value;
        audioMixer.SetFloat("MusicAudio", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("Music", volume);
    }
    public void SetSFXVolume()
    {
        float volume = sfxAudio.value;
        audioMixer.SetFloat("SFXAudio", Mathf.Log10(volume)* 20);
        PlayerPrefs.SetFloat("SFX", volume);
    }
    public void SetMasterVolume()
    {
        float volume = masterAudio.value;
        audioMixer.SetFloat("MasterAudio", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("Master", volume );
    }
    private void LoadVolume()
    {
        masterAudio.value = PlayerPrefs.GetFloat("Master", 0.75f);
        SetMasterVolume();
        musicAudio.value = PlayerPrefs.GetFloat("Music", 0.75f);
        SetMusicVolume();
        sfxAudio.value = PlayerPrefs.GetFloat("SFX", 0.75f);
        SetSFXVolume();
    }
}
