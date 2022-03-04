using UnityEngine;

public class HideWall : MonoBehaviour
{
    public GameObject player;
    public GameObject transparentWall;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameManager.instance.player.gameObject)
        {
            transparentWall.GetComponent<MeshRenderer>().enabled = false;
        }
    }

}
