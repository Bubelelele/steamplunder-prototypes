using UnityEngine;

public class GetHammerTrigger : MonoBehaviour
{
    public GameObject pos;
    public GameObject hammer;
    public ParticleSystem fog;
    public ParticleSystem explotion;
    public GameObject hammerMessage;
    public Animator hammerAnim;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameManager.instance.player.gameObject)
        {
            GameManager.instance.player.GetComponent<GearManager>().no = true;
            pos.SetActive(false);
            hammer.SetActive(false);
            fog.Stop();
            explotion.Play();
            Invoke("Used", 0.4f);
            hammerMessage.SetActive(true);
            hammerMessage.GetComponent<Animator>().SetBool("ShowMessage", true);
            hammerAnim.SetBool("HammerTaken", true);
        }
    }
    private void Used()
    {
        gameObject.SetActive(false);
    }
}
