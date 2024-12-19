using UnityEngine;
using UnityEngine.UI;

public class VolumeMenu : MonoBehaviour
{
    [SerializeField] private Slider ambienceSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider environmentSlider;

    private void Start()
    {
        ambienceSlider.value = AudioManager.Instance.AmbienceVolume;
        sfxSlider.value = AudioManager.Instance.SFXVolume;
        masterSlider.value = AudioManager.Instance.MasterVolume;
        environmentSlider.value = AudioManager.Instance.environmentAudioSourceVolume;


        ambienceSlider.onValueChanged.AddListener(AudioManager.Instance.SetAmbienceVolume);
        sfxSlider.onValueChanged.AddListener(AudioManager.Instance.SetSFXVolume);
        environmentSlider.onValueChanged.AddListener(AudioManager.Instance.SetenvironmentAudioSourceVolume);
        masterSlider.onValueChanged.AddListener(AudioManager.Instance.SetMasterVolume);

    }
}
