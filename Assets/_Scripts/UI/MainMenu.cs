using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    
    [SerializeField] private Animator animator;
    
    public void PlayButton() {
        animator.SetTrigger("play");
        StartCoroutine(LoadSequence());
    }

    private IEnumerator LoadSequence() {
        Time.timeScale = 10f;
        yield return new WaitForSecondsRealtime(2.1f);
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void QuitButton() {
        Application.Quit();
    }
    
}