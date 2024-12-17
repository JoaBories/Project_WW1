using UnityEngine;
using UnityEngine.UI;

public class VolumeMenu : MonoBehaviour
{
    [SerializeField] private Slider ambienceSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        ambienceSlider.value = AudioManager.Instance.AmbienceVolume;
        sfxSlider.value = AudioManager.Instance.SFXVolume;

        ambienceSlider.onValueChanged.AddListener(AudioManager.Instance.SetAmbienceVolume);
        sfxSlider.onValueChanged.AddListener(AudioManager.Instance.SetSFXVolume);
    }
}
