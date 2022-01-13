using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Slider _volumeSlider;
    [Header("Audio")]
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _clickSound;

    public static float audioVolume = 0.5f;

    private void Start()
    {
        _volumeSlider.value = audioVolume;
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
       //effect directional light
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
