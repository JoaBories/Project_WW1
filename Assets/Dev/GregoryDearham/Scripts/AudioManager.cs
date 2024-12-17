using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource ambienceAudioSource;
    [SerializeField] private AudioSource sfxAudioSource;

    private Dictionary<string, AudioClip> ambienceClips;
    private Dictionary<string, AudioClip> sfxClips;

    
    public float AmbienceVolume { get; private set; } = 1f;
    public float SFXVolume { get; private set; } = 1f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        ambienceClips = new Dictionary<string, AudioClip>();
        sfxClips = new Dictionary<string, AudioClip>();
    }

    private void Start()
    {
        // Load saved volumes
        AmbienceVolume = PlayerPrefs.GetFloat("AmbienceVolume", 1f);
        SFXVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);

        if (ambienceAudioSource) ambienceAudioSource.volume = AmbienceVolume;
        if (sfxAudioSource) sfxAudioSource.volume = SFXVolume;
    }

    public void SetAmbienceVolume(float volume)
    {
        AmbienceVolume = volume;
        if (ambienceAudioSource) ambienceAudioSource.volume = volume;
        PlayerPrefs.SetFloat("AmbienceVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        SFXVolume = volume;
        if (sfxAudioSource) sfxAudioSource.volume = volume;
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void PlayAmbience(string clipName)
    {
        if (ambienceClips == null)
        {
            Debug.LogWarning("Ambience clips dictionary is not initialized.");
            return;
        }

        if (ambienceAudioSource == null)
        {
            Debug.LogWarning("Ambience audio source is not assigned.");
            return;
        }

        if (ambienceClips.ContainsKey(clipName))
        {
            var clip = ambienceClips[clipName];
            if (ambienceAudioSource.clip != clip)
            {
                ambienceAudioSource.clip = clip;
                ambienceAudioSource.loop = true;
                ambienceAudioSource.Play();
            }
        }
        else
        {
            Debug.LogWarning($"Ambience clip '{clipName}' not found.");
        }
    }

    public void PlaySFX(string clipName)
    {
        if (sfxClips.ContainsKey(clipName) && sfxAudioSource)
        {
            sfxAudioSource.PlayOneShot(sfxClips[clipName]);
        }
    }

    public void StopAmbience()
    {
        if (ambienceAudioSource) ambienceAudioSource.Stop();
    }

    public void InitializeClips(Dictionary<string, AudioClip> ambience, Dictionary<string, AudioClip> sfx)
    {
        ambienceClips = ambience;
        sfxClips = sfx;
    }
}
