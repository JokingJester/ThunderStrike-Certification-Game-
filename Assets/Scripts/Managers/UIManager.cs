using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Michsky.UI.ModernUIPack;
using TMPro;

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

    [Header("Waves UI")]
    [SerializeField] private string[] _waveMessages;
    [SerializeField] private TMP_Text _waveText;
    [SerializeField] private TMP_Text _waveMessageText;
    [SerializeField] private ModalWindowManager _modal;

    [Header("Weapon Level UI")]
    [SerializeField] private TMP_Text _weaponLevelText;


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

    IEnumerator CloseModelWindow()
    {
        yield return new WaitForSeconds(5);
        _modal.CloseWindow();
    }
}
