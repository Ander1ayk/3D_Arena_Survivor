using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
   public static AudioManager Instance;

    [Header("Mixer Groups")]
    [SerializeField] private AudioMixerGroup sfxGroup;

    private void Awake()
    {
        if(Instance == null) Instance = this;
    }

    public void PlaySFX(AudioClip audioClip, bool pitchRandom = true, float volume = 1f)
    {
        if (audioClip == null) return;

        GameObject go = new GameObject("TempSFX" + audioClip.name);
        AudioSource source = go.AddComponent<AudioSource>();

        source.clip = audioClip;
        source.outputAudioMixerGroup = sfxGroup;
        source.volume = volume;

        if(pitchRandom)
        {
            source.pitch = Random.Range(0.85f, 1.2f);
        }
        source.Play();
        Destroy(go, audioClip.length);
    }
}
