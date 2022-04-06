using UnityEngine;

public class SpinTrigger : MonoBehaviour
{
    public GameObject spin;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameManager.instance.player.gameObject)
        {
            GameManager.instance.player.gameObject.transform.parent = spin.transform;
        }
        if (other.gameObject.tag == "MovingBox")
        {
            other.transform.parent.gameObject.transform.parent = spin.transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == GameManager.instance.player.gameObject)
        {
            GameManager.instance.player.gameObject.transform.parent = null;
        }
        if (other.gameObject.tag == "MovingBox")
        {
            other.transform.parent.gameObject.transform.parent = null;
        }
    }
}
