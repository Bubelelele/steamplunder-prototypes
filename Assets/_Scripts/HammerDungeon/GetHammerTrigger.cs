using UnityEngine;

public class GetHammerTrigger : MonoBehaviour
{
    public GameObject hammer;
    public ParticleSystem fog;
    public ParticleSystem explotion;
    public GameObject hammerMessage;
    public Animator hammerAnim;
    public AudioSource artifactPickupAudioSource;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameManager.instance.player.gameObject)
        {
            GameManager.instance.player.GetComponent<GearManager>().ToggleHammer(true);
            hammer.SetActive(false);
            fog.Stop();
            explotion.Play();
            Invoke("Used", 0.4f);
            hammerMessage.SetActive(true);
            hammerMessage.GetComponent<Animator>().SetBool("ShowMessage", true);
            hammerAnim.SetBool("HammerTaken", true);
            artifactPickupAudioSource.Play();
        }
    }
    private void Used()
    {
        gameObject.SetActive(false);
    }
}
