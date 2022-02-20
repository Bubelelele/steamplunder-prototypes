using UnityEngine;

public class DestroyRock : MonoBehaviour
{
    public GameObject Rock;
    public GameObject player;
    public ParticleSystem rockDebrees;
    public ParticleSystem dustWave;
    private void OnTriggerStay(Collider other)
    {
        
        if (other.gameObject == GameManager.instance.player.gameObject && Input.GetKey(KeyCode.F) && GameManager.instance.player.GetComponent<GearManager>().no)
        {
            Invoke("HideRock", 0.3f);
            Invoke("TurnOffRock", 0.7f);
        }
    }
    private void HideRock()
    {
        Rock.GetComponent<MeshRenderer>().enabled = false;
        rockDebrees.Play();
        dustWave.Play();
    }
    private void TurnOffRock()
    {
        Rock.SetActive(false);
    }
}
