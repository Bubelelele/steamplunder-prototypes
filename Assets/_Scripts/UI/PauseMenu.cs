using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    [SerializeField] private GameObject pauseMenuUI;
    
    private bool _gamePaused;
    private GameState _previousState;

    private void Update() {
        if (Input.GetKeyDown(InputManager.instance.PauseBtn)) {
            if (_gamePaused) Resume();
            else Pause();
        }
    }

    private void Pause() {
        pauseMenuUI.SetActive(true);
        _previousState = GameManager.instance.state;
        GameManager.instance.UpdateGameState(GameState.Paused);
        _gamePaused = true;
    }

    public void Resume() {
        pauseMenuUI.SetActive(false);
        GameManager.instance.UpdateGameState(_previousState);
        _gamePaused = false;
    }

    public void RestartScene() {
        Resume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitApplication() => Application.Quit();
}