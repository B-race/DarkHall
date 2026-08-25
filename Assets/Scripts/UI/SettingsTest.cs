using UnityEngine;
using UnityEngine.UI;

public class SettingsTest : MonoBehaviour
{
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    [SerializeField] private AudioSource bgmAudioSource;
    [SerializeField] private AudioSource sfxAudioSource;

    private void Start()
    {
        bgmSlider.minValue = 0f;
        bgmSlider.maxValue = 1f;

        sfxSlider.minValue = 0f;
        sfxSlider.maxValue = 1f;

        bgmSlider.onValueChanged.AddListener(ChangeBGMVolume);
        sfxSlider.onValueChanged.AddListener(ChangeSFXVolume);

        ChangeBGMVolume(bgmSlider.value);
        ChangeSFXVolume(sfxSlider.value);
    }

    private void ChangeBGMVolume(float value)
    {
        if (bgmAudioSource != null)
        {
            bgmAudioSource.volume = value;
        }

        Debug.Log("BGM Volume: " + value);
    }

    private void ChangeSFXVolume(float value)
    {
        if (sfxAudioSource != null)
        {
            sfxAudioSource.volume = value;
        }

        Debug.Log("SFX Volume: " + value);
    }
}