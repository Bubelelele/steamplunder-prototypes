using UnityEngine;

public class DestroyRock : MonoBehaviour
{
    public GameObject Rock;
    public GameObject player;
    public ParticleSystem rockDebrees;
    public ParticleSystem dustWave;
    public AudioSource destroyRockAudioSource;
    private void OnTriggerStay(Collider other)
    {
        
        if (other.gameObject == GameManager.instance.player.gameObject && Input.GetKey(KeyCode.F) && GameManager.instance.player.GetComponent<GearManager>().HammerActive)
        {
            Invoke("HideRock", 0.3f);
            Invoke("TurnOffRock", 1.3f);
        }
    }
    private void HideRock()
    {
        Rock.GetComponent<MeshRenderer>().enabled = false;
        rockDebrees.Play();
        dustWave.Play();
        destroyRockAudioSource.Play();
    }
    private void TurnOffRock()
    {
        Rock.SetActive(false);
    }
}
