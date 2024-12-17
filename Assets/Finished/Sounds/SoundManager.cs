using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource _audioSource;

    [SerializeField] private List<AudioClip> Sounds = new List<AudioClip>();
    [SerializeField] private List<string> SoundsNames = new List<string>();

    private void Awake()
    {
        instance = this;
    }

    public void PlaySound(string soundName, float volume, Transform spawnTransform)
    {
        AudioSource audioSource = Instantiate(_audioSource, spawnTransform.position, Quaternion.identity);
        audioSource.clip = Sounds[SoundsNames.IndexOf(soundName)];
        audioSource.volume = volume;
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }
}
