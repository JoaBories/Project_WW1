using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource ambienceAudioSource;
    [SerializeField] private AudioSource sfxAudioSource;

    [Header("Volume Sliders")]
    [SerializeField] private Slider ambienceSlider;
    [SerializeField] private Slider sfxSlider;

    [Header("Ambience Clips")]
    public AudioClip backgroundDayTrenchies;
    public AudioClip backgroundNightTrenchies;
    public AudioClip backgroundBunkerEcho;

    [Header("Player Sound Effects")]
    public AudioClip jumpStart;
    public AudioClip pickUpItem;
    // Add other clips here...

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
        }
    }

    private void Start()
    {
        // Initialize sliders with saved volume settings or defaults
        float savedAmbienceVolume = PlayerPrefs.GetFloat("AmbienceVolume", 1f);
        float savedSFXVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);

        SetAmbienceVolume(savedAmbienceVolume);
        SetSFXVolume(savedSFXVolume);

        if (ambienceSlider != null)
        {
            ambienceSlider.value = savedAmbienceVolume;
            ambienceSlider.onValueChanged.AddListener(SetAmbienceVolume);
        }

        if (sfxSlider != null)
        {
            sfxSlider.value = savedSFXVolume;
            sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        }
    }

    public void SetAmbienceVolume(float volume)
    {
        if (ambienceAudioSource != null)
        {
            ambienceAudioSource.volume = volume;
        }
        PlayerPrefs.SetFloat("AmbienceVolume", volume); // Save the value
    }

    public void SetSFXVolume(float volume)
    {
        if (sfxAudioSource != null)
        {
            sfxAudioSource.volume = volume;
        }
        PlayerPrefs.SetFloat("SFXVolume", volume); // Save the value
    }

    #region Sound Playback

    public void PlayAmbience(AudioClip ambienceClip)
    {
        if (ambienceClip == null || ambienceAudioSource == null) return;

        if (ambienceAudioSource.clip != ambienceClip)
        {
            ambienceAudioSource.clip = ambienceClip;
            ambienceAudioSource.loop = true;
            ambienceAudioSource.Play();
        }
    }

    public void StopAmbience()
    {
        if (ambienceAudioSource != null && ambienceAudioSource.isPlaying)
        {
            ambienceAudioSource.Stop();
            ambienceAudioSource.clip = null;
        }

    }

    public void PlaySFX(AudioClip sfxClip)
    {
        if (sfxClip != null && sfxAudioSource != null)
        {
            sfxAudioSource.PlayOneShot(sfxClip);
        }
    }

    #endregion
}
