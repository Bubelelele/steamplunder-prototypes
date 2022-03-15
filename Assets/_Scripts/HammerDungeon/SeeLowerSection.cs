using UnityEngine;

public class SeeLowerSection : MonoBehaviour
{
    //public GameObject lowerSection;
    public GameObject transparentWall;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameManager.instance.player.gameObject)
        {
            //lowerSection.SetActive(true);
            transparentWall.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
