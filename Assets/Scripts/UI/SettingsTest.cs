using UnityEngine;
using UnityEngine.UI;

public class SettingsTest : MonoBehaviour
{
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private AudioSource bgmAudioSource;
    [SerializeField] private AudioSource sfxAudioSource;

    private const string BGM_VOLUME_KEY = "BGM_VOLUME";
    private const string SFX_VOLUME_KEY = "SFX_VOLUME";

    private void Start()
    {
        bgmSlider.minValue = 0f;
        bgmSlider.maxValue = 1f;

        sfxSlider.minValue = 0f;
        sfxSlider.maxValue = 1f;

        float savedBGMVolume =
            PlayerPrefs.GetFloat(BGM_VOLUME_KEY, 0.5f);

        float savedSFXVolume =
            PlayerPrefs.GetFloat(SFX_VOLUME_KEY, 0.5f);

        bgmSlider.value = savedBGMVolume;
        sfxSlider.value = savedSFXVolume;

        ChangeBGMVolume(savedBGMVolume);
        ChangeSFXVolume(savedSFXVolume);

        bgmSlider.onValueChanged.AddListener(ChangeBGMVolume);
        sfxSlider.onValueChanged.AddListener(ChangeSFXVolume);
    }

    private void ChangeBGMVolume(float value)
    {
        if (bgmAudioSource != null)
        {
            bgmAudioSource.volume = value;
        }

        PlayerPrefs.SetFloat(BGM_VOLUME_KEY, value);
        PlayerPrefs.Save();

        Debug.Log("BGM Volume: " + value);
    }

    private void ChangeSFXVolume(float value)
    {
        if (sfxAudioSource != null)
        {
            sfxAudioSource.volume = value;
        }

        PlayerPrefs.SetFloat(SFX_VOLUME_KEY, value);
        PlayerPrefs.Save();

        Debug.Log("SFX Volume: " + value);
    }
}