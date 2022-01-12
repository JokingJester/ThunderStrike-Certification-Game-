using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Slider _brightnessSlider;
    [Header("Audio")]
    [SerializeField] private AudioClip _clickSound;
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
}
