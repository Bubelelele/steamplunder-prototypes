using UnityEngine;
using UnityEngine.Playables;

public class StartCutscene : MonoBehaviour
{
    [SerializeField] private Canvas gameCanvas;
    [SerializeField] private Animator cameraFadeAnim;
    [SerializeField] private GameObject axeLv1;
    [SerializeField] private GameObject axeLv2;
    public void FadeOut()
    {
        gameCanvas.enabled = false;
        cameraFadeAnim.SetBool("FadeOut", true);
        Invoke("BeginCutscene", 1f);
        GameManager.instance.UpdateGameState(GameState.NoMove);
    }
    private void BeginCutscene()
    {
        gameObject.GetComponent<PlayableDirector>().enabled = true;
        Invoke("FadeIn", 12f);
    }
    private void FadeIn()
    {
        axeLv1.SetActive(false);
        axeLv2.SetActive(true);
        gameObject.GetComponent<PlayableDirector>().enabled = false;
        cameraFadeAnim.SetBool("FadeOut", false);
        Invoke("EnableCanvas", 0.5f);
    }
    private void EnableCanvas()
    {
        gameCanvas.enabled = true;
        GameManager.instance.UpdateGameState(GameState.Default);
    }
}
