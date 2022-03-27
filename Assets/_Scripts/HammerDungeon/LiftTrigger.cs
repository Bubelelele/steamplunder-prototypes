using UnityEngine;

public class LiftTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == GameManager.instance.player.gameObject)
        {
            GameManager.instance.player.gameObject.transform.parent = transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == GameManager.instance.player.gameObject)
        {
            GameManager.instance.player.gameObject.transform.parent = null;
        }
    }
}
