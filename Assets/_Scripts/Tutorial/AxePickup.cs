using UnityEngine;

public class AxePickup : MonoBehaviour
{
    public GameObject axe;
    public GameObject axeInHand;
    public ParticleSystem explotion;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameManager.instance.player.gameObject)
        {
            GameManager.instance.player.GetComponent<GearManager>().ToggleAxe(true);
            axeInHand.SetActive(true);
            axe.SetActive(false);
            explotion.Play();
            Invoke("Used", 0.4f);
        }
    }
    private void Used()
    {
        gameObject.SetActive(false);
    }
}
