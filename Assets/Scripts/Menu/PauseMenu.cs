using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private AudioClip _clickSound;
    public void RestartGame()
    {
        AudioManager.Instance.PlayOneShot(_clickSound, 12f);
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void BackToMainMenu()
    {
        AudioManager.Instance.PlayOneShot(_clickSound, 12f);
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
