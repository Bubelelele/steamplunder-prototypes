using UnityEngine;

public class HideLowerSection : MonoBehaviour
{
    public GameObject player;
    public GameObject lowerSection;
    public GameObject transparentWall;
    public GameObject pos;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == GameManager.instance.player.gameObject)
        {
            lowerSection.SetActive(false);
            transparentWall.GetComponent<MeshRenderer>().enabled = false;
            GameManager.instance.player.GetComponent<GearManager>().no = true;
            pos.SetActive(false);
        }
    }
}
