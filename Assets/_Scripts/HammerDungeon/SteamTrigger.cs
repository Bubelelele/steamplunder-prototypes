using UnityEngine;

public class SteamTrigger : MonoBehaviour
{
    public ParticleSystem heatParticle;
    public ParticleSystem steamParticle;
    public Animator DoorAnim;
    public AudioSource doorAudioSource;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "MovingBox")
        {
            other.transform.position = transform.position;
            other.gameObject.GetComponent<MetalBox>().enabled = false;
            heatParticle.Stop();
            steamParticle.Stop();
            DoorAnim.SetBool("OpenDoor", true);
            doorAudioSource.Play();
        }
    }
}
