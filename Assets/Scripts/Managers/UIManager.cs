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


    private void Start()
    {
        _slider.maxValue = _sliderMaxValue;
        AddScore(0);
    }
    public void DisplayCurrentWave(int waveNumer)
    {
        _waveText.text = "Wave " + waveNumer;
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
        _scoreText.text = "Score: " + _score;
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

    IEnumerator CloseModelWindow()
    {
        yield return new WaitForSeconds(5);
        _modal.CloseWindow();
    }
}