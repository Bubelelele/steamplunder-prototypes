using UnityEngine;

public class GetGrappleTrigger : MonoBehaviour
{
    public GameObject grappleObject;
    public ParticleSystem fog;
    public ParticleSystem explosion;
    public GameObject message;
    public AudioSource artifactPickupAudioSource;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameManager.instance.player.gameObject)
        {
            GameManager.instance.player.GetComponent<GearManager>().ToggleGrapple(true);
            grappleObject.SetActive(false);
            fog.Stop();
            explosion.Play();
            Invoke("Used", 0.4f);
            message.SetActive(true);
            message.GetComponent<Animator>().SetBool("ShowMessage", true);
            artifactPickupAudioSource.Play();
        }
    }
    private void Used()
    {
        gameObject.SetActive(false);
    }
}
