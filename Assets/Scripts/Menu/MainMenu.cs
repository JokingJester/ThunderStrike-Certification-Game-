using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public EventSystem eventSystem;
    public Button startButton;
    public Button optionButton;
    public Button backButton;
    private Button _gamepadSelectedButton;

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
        if(Gamepad.current != null)
            optionButton.Select();
    }

    public void OptionButton()
    {
        if (Gamepad.current != null)
            eventSystem.SetSelectedGameObject(backButton.gameObject);
    }
    public void ChangeControlScheme()
    {
        if (_input.currentControlScheme == "Keyboard")
        {
            Debug.Log("Scheme is keyboard and mouse");
        }
        else if (_input.currentControlScheme == "Gamepad")
        {
            Debug.Log("Scheme is gamepad");
            Debug.Log(eventSystem.currentSelectedGameObject);
            if (_gamepadSelectedButton != null)
                _gamepadSelectedButton.Select();
            
        }
    }

    public void SetSelectedButton(Button button)
    {
        //eventSystem.currentSelectedGameObject
        _gamepadSelectedButton = button;
    }
}
