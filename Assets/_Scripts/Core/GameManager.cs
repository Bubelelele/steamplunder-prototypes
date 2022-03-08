using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
	private void Awake()
	{
		instance = this;
        player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	public GameState state;
    public static event Action<GameState> OnGameStateChanged;
    [HideInInspector] public Transform player;
    
    private void Start() {
        UpdateGameState(GameState.Default);
    }

    private void Update() {
        if (Input.GetKeyDown(InputManager.instance.ReloadBtn)) ReloadScene();
    }

    public void UpdateGameState(GameState newState) {
        if (state == newState) return;
        state = newState;

        switch (newState) {
            case GameState.Default:
                HandleDefault();
                break;
            case GameState.Interaction:
                HandleInteraction();
                break;
            case GameState.NoMove:
                HandleNoMove();
                break;
            case GameState.Paused:
                HandlePaused();
                break;
            default:
                Debug.LogWarning("No state of type " + newState);
                break;
        }

        OnGameStateChanged?.Invoke(newState);
    }
    
    private void HandleDefault() {
        Debug.Log("Default State");
        Time.timeScale = 1f;
    }
    
    private void HandleInteraction() {
        Debug.Log("Interaction State");
        Time.timeScale = 0f;
    }

    private void HandleNoMove() {
        Debug.Log("NoMove State");
        Time.timeScale = 1f;
    }

    private void HandlePaused() {
        Debug.Log("Paused State");
        Time.timeScale = 0f;
    }
    
    public void LoadScene(int buildIndex) {
        Time.timeScale = 1f;
        SceneManager.LoadScene(buildIndex);
    }
    
    public void ReloadScene() => LoadScene(SceneManager.GetActiveScene().buildIndex);

    public void WaitReloadScene(float delay) => StartCoroutine(WaitReload(delay));
    
    private IEnumerator WaitReload(float delay) {
        yield return new WaitForSeconds(delay);
        ReloadScene();
    }
}

public enum GameState {
    Default,
    Interaction,
    NoMove,
    Paused
}