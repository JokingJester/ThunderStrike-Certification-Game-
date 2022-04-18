using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class MainMenu : MonoBehaviour
{
    public EventSystem eventSystem;
    public Button startButton;
    public Button optionButton;
    public Button backButton;

    [Header("Sliders")]
    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private Slider _brightnessSlider;
    [Header("Audio")]
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _clickSound;

    public static float audioVolume = 0.5f;
    public static float brightness = 1f;

    public PlayerInput _input;
    private void Start()
    {
        _volumeSlider.value = audioVolume;
        _brightnessSlider.value = brightness;
        if (Gamepad.current != null)
        {
            eventSystem.firstSelectedGameObject = startButton.gameObject;
        }
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

    public void BackButton()
    {
        if (_input.currentControlScheme == "Gamepad")
            optionButton.Select();
        else
            eventSystem.SetSelectedGameObject(null);
    }

    public void OptionButton()
    {
        if (_input.currentControlScheme == "Gamepad")
            eventSystem.SetSelectedGameObject(backButton.gameObject);
        else
            eventSystem.SetSelectedGameObject(null);
    }
    public void ChangeControlScheme()
    {
        if (_input.currentControlScheme == "Gamepad")
        {
            if (eventSystem.currentSelectedGameObject == null)
            {
                if (backButton.gameObject.activeInHierarchy == false)
                    startButton.Select();
                else
                    backButton.Select();
            }
            
        }
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
            _input.SwitchCurrentControlScheme("Keyboard");
    }
}
