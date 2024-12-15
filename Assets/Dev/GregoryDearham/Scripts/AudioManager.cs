using UnityEngine;
using UnityEngine.UI; // For Slider
using UnityEngine.Audio; // Optional, for AudioMixer integration

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    //AudioManager.Instance.PlaySFX(AudioManager.Instance.jumpStart); as an example for SFX
    //AudioManager.Instance.PlayAmbience(AudioManager.Instance.backgroundDayTrenchies); as an example for ambience


    [Header("Audio Sources")]
    [SerializeField] public AudioSource ambienceAudioSource; // For ambience sounds
    [SerializeField] public AudioSource sfxAudioSource; // For sound effects

    [Header("Volume Sliders")]
    [SerializeField] public Slider ambienceSlider; // Slider for ambience volume
    [SerializeField] public Slider sfxSlider; // Slider for sound effects volume

    [Header("Ambience Clips")]
    [SerializeField] public AudioClip backgroundDayTrenchies;
    [SerializeField] public AudioClip backgroundNightTrenchies;
    [SerializeField] public AudioClip backgroundBunkerEcho;

    [Header("Player Sound Effects")]
    [SerializeField] public AudioClip walkFootstep;
    [SerializeField] public AudioClip runFootstep;
    [SerializeField] public AudioClip climb;
    [SerializeField] public AudioClip jumpStart;
    [SerializeField] public AudioClip jumpFall;
    [SerializeField] public AudioClip pickUpItem;
    [SerializeField] public AudioClip breathingInMask;
    [SerializeField] public AudioClip coughingMustardGas;

    [Header("Environmental Sound Effects")]
    [SerializeField] public AudioClip electricityZapCharge;
    [SerializeField] public AudioClip electricityZapAttack;
    [SerializeField] public AudioClip switchLights;
    [SerializeField] public AudioClip lightOff;
    [SerializeField] public AudioClip lightOn;
    [SerializeField] public AudioClip doorOpen;

    [Header("Combat & NPC Sound Effects")]
    [SerializeField] public AudioClip die;
    [SerializeField] public AudioClip beDetected;
    [SerializeField] public AudioClip pushBox;
    [SerializeField] public AudioClip boxFallGround;
    [SerializeField] public AudioClip destroyRadio;
    [SerializeField] public AudioClip enemyFallStun;
    [SerializeField] public AudioClip sufferingGuy;
    [SerializeField] public AudioClip killSoldier;

    [Header("UI & Cinematic Sound Effects")]
    [SerializeField] public AudioClip speechBubbleSpawn;
    [SerializeField] public AudioClip openMap;
    [SerializeField] public AudioClip introCinematics;
    [SerializeField] public AudioClip ambushCinematics;

    [Header("Weapon Sound Effects")]
    [SerializeField] public AudioClip shootSFX;
    [SerializeField] public AudioClip stepGiantRobotSFX;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); // Destroy duplicates
        }
    }



    private void Start()
    {
        // Initialize sliders with saved volume settings or default values
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
        if (ambienceClip != null && ambienceAudioSource != null)
        {
            ambienceAudioSource.clip = ambienceClip;
            ambienceAudioSource.loop = true;
            ambienceAudioSource.Play();
        }
    }

    public void StopAmbience()
    {
        if (ambienceAudioSource != null)
        {
            ambienceAudioSource.Stop();
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
