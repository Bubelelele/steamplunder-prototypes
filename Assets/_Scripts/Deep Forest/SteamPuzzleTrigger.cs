using UnityEngine;

public class SteamPuzzleTrigger : MonoBehaviour
{
    public ParticleSystem[] steam;
    public GameObject pressurePlateManager;

    private void OnTriggerEnter(Collider other)
    {
        if (!pressurePlateManager.GetComponent<OverworldPuzzle>().done)
        {
            if (other.gameObject == GameManager.instance.player.gameObject && gameObject.name == pressurePlateManager.GetComponent<OverworldPuzzle>().currentPlateToStepOn.ToString())
            {
                pressurePlateManager.GetComponent<OverworldPuzzle>().Correct();
                StopSteam();
            }
            else if (other.gameObject == GameManager.instance.player.gameObject && gameObject.name != pressurePlateManager.GetComponent<OverworldPuzzle>().currentPlateToStepOn.ToString())
            {
                pressurePlateManager.GetComponent<OverworldPuzzle>().Wrong();
            }
        }
    }

    public void StopSteam()
    {
        for (int i = 0; i < steam.Length; i++)
        {
            steam[i].Stop();
        }
    }
    public void StartSteam()
    {
        for (int i = 0; i < steam.Length; i++)
        {
            steam[i].Play();
        }
    }
}
