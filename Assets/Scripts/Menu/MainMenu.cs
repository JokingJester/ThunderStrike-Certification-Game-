using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private Slider _brightnessSlider;
    [Header("Audio")]
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _clickSound;

    public static float audioVolume = 0.5f;
    public static float brightness = 1f;

    private void Start()
    {
        _volumeSlider.value = audioVolume;
        _brightnessSlider.value = brightness;
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void AdjustBrightness()
    {
        brightness = _brightnessSlider.value;
    }

    public void PlayClickSound()
    {
        AudioManager.Instance.PlayOneShot(_clickSound, 12);
    }

    public void ChangeAudio()
    {
        _source.volume = _volumeSlider.value;
        audioVolume = _volumeSlider.value;
    }
}
