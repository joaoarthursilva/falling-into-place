using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    [SerializeField] private bool isPaused = false;
    private bool _togglePause = false;

    void Start()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }

    public void OpenSettings()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void CloseSettings()
    {
        pauseMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        Time.timeScale = 1;
    }

    private void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;
        pauseMenu.gameObject.SetActive(true);
        settingsMenu.gameObject.SetActive(false);
    }

    public void TogglePause(bool togglePause)
    {
        switch (isPaused)
        {
            case false when togglePause: //esc to pause
                PauseGame();
                break;
            case true when togglePause: //esc to unpause
                ResumeGame();
                break;
        }
    }

    void Update()
    {
        _togglePause = Input.GetKeyDown(KeyCode.Escape);
        TogglePause(_togglePause);
    }
}