using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Audio;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource ambienceAudioSource;
    [SerializeField] private AudioSource sfxAudioSource;
    [SerializeField] private AudioSource environmentAudioSource;
    //[SerializeField] private AudioSource sfxAudioSource;

    private Dictionary<string, AudioClip> ambienceClips;
    private Dictionary<string, AudioClip> sfxClips;
    private Dictionary<string, AudioClip> environmentClips;


    public float AmbienceVolume { get; private set; } = 1f;
    public float SFXVolume { get; private set; } = 1f;

    public float environmentAudioSourceVolume { get; private set; } = 1f;

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
        environmentClips = new Dictionary<string, AudioClip>();
}

    private void Start()
    {
        // Load saved volumes
        AmbienceVolume = PlayerPrefs.GetFloat("AmbienceVolume", 1f);
        SFXVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);

        if (ambienceAudioSource) ambienceAudioSource.volume = AmbienceVolume;
        if (sfxAudioSource) sfxAudioSource.volume = SFXVolume;
        if (environmentAudioSource) environmentAudioSource.volume = environmentAudioSourceVolume;

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

    public void SetenvironmentAudioSourceVolume(float volume)
    {
        SFXVolume = volume;
        if (environmentAudioSource) environmentAudioSource.volume = volume;
        PlayerPrefs.SetFloat("EnvironmentVolume", volume);
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


    public void PlayEnvironment (string clipName)
    {
        
        if (environmentClips.ContainsKey(clipName) && environmentAudioSource)
        {
            environmentAudioSource.PlayOneShot(environmentClips[clipName]);
            environmentAudioSource.loop = true;

        }

    }

    public void StopEnvironment(string clipName)
    {

        if (environmentAudioSource) environmentAudioSource.Stop();

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

    public void StopSFX(string clipName)
    {
        // Check if the current clip's name matches the specified clip name 
        if (sfxClips.ContainsKey(clipName) && sfxAudioSource)
        {
            Debug.Log("Audio is Working");
            sfxAudioSource.Stop();
        }
    }




    public void InitializeClips(Dictionary<string, AudioClip> ambience, Dictionary<string, AudioClip> sfx, Dictionary<string, AudioClip> environment)
    {
        ambienceClips = ambience;
        sfxClips = sfx;
        environmentClips = environment;
    }
}
