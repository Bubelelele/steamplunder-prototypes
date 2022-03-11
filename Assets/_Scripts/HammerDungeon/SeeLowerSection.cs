using UnityEngine;

public class SeeLowerSection : MonoBehaviour
{
    public GameObject player;
    public GameObject lowerSection;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameManager.instance.player.gameObject)
        {
            lowerSection.SetActive(true);
        }
    }
}
