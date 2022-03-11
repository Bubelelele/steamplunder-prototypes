using UnityEngine;

public class LiftTriggerSteam : MonoBehaviour
{
    public GameObject player;
    public GameObject lift;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == GameManager.instance.player.gameObject)
        {
            GameManager.instance.player.gameObject.transform.parent = lift.transform;
        }
        if(other.gameObject.tag == "MovingBox")
        {
            other.transform.parent.gameObject.transform.parent = lift.transform;
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
