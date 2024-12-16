using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SceneAudioManager : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        if (AudioManager.Instance == null)
        {
            Debug.LogError("AudioManager.Instance is null. Ensure AudioManager is initialized.");
            return;
        }

        LoadAudioLibraryAndInitialize();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlaySceneAudio(scene.name);
    }

    private void LoadAudioLibraryAndInitialize()
    {
        // Load the AudioClipLibrary
        AudioClipLibrary clipLibrary = Resources.Load<AudioClipLibrary>("AudioClipLibrary");
        if (clipLibrary == null)
        {
            Debug.LogError("AudioClipLibrary not found. Ensure it is placed in a Resources folder.");
            return;
        }

        // Populate dictionaries
        Dictionary<string, AudioClip> ambienceClips = new Dictionary<string, AudioClip>();
        Dictionary<string, AudioClip> sfxClips = new Dictionary<string, AudioClip>();

        foreach (var entry in clipLibrary.ambienceClips)
        {
            if (!string.IsNullOrEmpty(entry.name) && entry.clip != null)
                ambienceClips[entry.name] = entry.clip;
        }

        foreach (var entry in clipLibrary.sfxClips)
        {
            if (!string.IsNullOrEmpty(entry.name) && entry.clip != null)
                sfxClips[entry.name] = entry.clip;
        }

        // Initialize AudioManager with these clips
        AudioManager.Instance.InitializeClips(ambienceClips, sfxClips);
    }

    private void PlaySceneAudio(string sceneName)
    {
        // Play audio specific to each scene
        switch (sceneName)
        {
            case "GregsDevCorner":
                AudioManager.Instance.PlayAmbience("NightTrenchies");
                AudioManager.Instance.PlaySFX("DoorCreak"); // Example SFX
                break;
            case "GameOver":
                AudioManager.Instance.PlayAmbience("BunkerEcho");
                AudioManager.Instance.PlaySFX("BunkerRumble");
                break;
            default:
                AudioManager.Instance.StopAmbience();
                break;
        }
    }
}
