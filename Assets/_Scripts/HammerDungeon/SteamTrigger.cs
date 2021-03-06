using UnityEngine;

public class SteamTrigger : MonoBehaviour
{
    public ParticleSystem heatParticle;
    public ParticleSystem steamParticle;
    public Animator DoorAnim;
    public AudioSource doorAudioSource;
    public AudioSource steamAudioSource;
    public AudioClip steamShutAudioClip;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "MovingBox")
        {
            other.transform.position = transform.position;
            other.gameObject.GetComponent<MetalBox>().enabled = false;
            other.transform.parent.transform.parent = transform;
            heatParticle.Stop();
            steamParticle.Stop();
            DoorAnim.SetBool("OpenDoor", true);
            doorAudioSource.Play();
            steamAudioSource.Stop();
            steamAudioSource.clip = steamShutAudioClip;
            steamAudioSource.Play();
            steamAudioSource.loop = false;
        }
    }
}
