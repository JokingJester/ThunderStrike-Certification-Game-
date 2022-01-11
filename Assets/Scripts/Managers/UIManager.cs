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

    [SerializeField] private string[] _waveMessages;
    [SerializeField] private TMP_Text _waveText;
    [SerializeField] private TMP_Text _waveMessageText;
    [SerializeField] private ModalWindowManager _modal;


    public void DisplayCurrentWave(int waveNumer)
    {
        _waveText.text = "Wave " + waveNumer;
        _waveMessageText.text = _waveMessages[waveNumer - 1];
        //change text to wave + wavenumber
        //change message to string array [-1 wave number]. string
        //open model window
        _modal.OpenWindow();
        StartCoroutine(CloseModelWindow());
    }

    IEnumerator CloseModelWindow()
    {
        yield return new WaitForSeconds(5);
        _modal.CloseWindow();
    }
}
