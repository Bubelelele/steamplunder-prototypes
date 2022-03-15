using UnityEngine;

public class HideLowerSection : MonoBehaviour
{
    public GameObject lowerSection;
    public GameObject transparentWall;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == GameManager.instance.player.gameObject)
        {
            lowerSection.SetActive(false);
            transparentWall.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
