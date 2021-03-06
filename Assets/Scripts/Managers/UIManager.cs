using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Michsky.UI.ModernUIPack;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("UI Manager Is Null");

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    private int _score;
    private string wave = "Wave ";
    private string _scoreString = "Score: ";
    [Header("Waves UI")]
    [SerializeField] private string[] _waveMessages;
    [SerializeField] private TMP_Text _waveText;
    [SerializeField] private TMP_Text _waveMessageText;
    [SerializeField] private ModalWindowManager _modal;

    [Header("Weapon Level UI")]
    [SerializeField] private TMP_Text _weaponLevelText;

    [Header("Health UI")]
    [SerializeField] private Slider _slider;
    [SerializeField] private int _sliderMaxValue;

    [Header("Score UI")]
    [SerializeField] private TMP_Text _scoreText;

    [Header("Slow Time UI")]
    [SerializeField] private GameObject _pressQ;
    [SerializeField] private GameObject _waitIcon;

    [Header("Canvas")]
    [SerializeField] private Canvas _victoryCanvas;
    [SerializeField] private Canvas _loseCanvas;
    [SerializeField] private TMP_Text _finalScoreText;

    [Header("Use Checkpoint")]
    [SerializeField] private ModalWindowManager _useCheckpointWindow;

    [Header("Directional Light")]
    [SerializeField] Light _directionalLight;



    private void Start()
    {
        _directionalLight.intensity = MainMenu.brightness;
        _slider.maxValue = _sliderMaxValue;
        AddScore(0);
    }
    public void DisplayCurrentWave(int waveNumer)
    {
        _waveText.text = wave + waveNumer;
        _waveMessageText.text = _waveMessages[waveNumer - 1];
        _modal.OpenWindow();
        StartCoroutine(CloseModelWindow());
    }

    public void DisplayCurrentWeapon(int weaponLevel, string weaponName)
    {
        _weaponLevelText.text = "Level " + weaponLevel + ": \n" + weaponName;
    }

    public void DisplayHealth(float health)
    {
        _slider.value = health + 1;
    }

    public void AddScore(int score)
    {
        _score += score;
        _scoreText.text = _scoreString + _score;
    }

    public void ShowQAbility()
    {
        _waitIcon.SetActive(false);
        _pressQ.SetActive(true);
    }

    public void DontShowQAbility()
    {
        _waitIcon.SetActive(true);
        _pressQ.SetActive(false);
    }

    public void ShowVictoryCanvas()
    {
        _finalScoreText.text = "Your final score is " + _score;
        _victoryCanvas.enabled = true;
    }

    public void OpenUseCheckpointWindow()
    {
        _useCheckpointWindow.OpenWindow();
    }

    public void ShowLoseScreen()
    {
        _loseCanvas.enabled = true;
    }

    IEnumerator CloseModelWindow()
    {
        yield return new WaitForSeconds(5);
        _modal.CloseWindow();
    }
}
