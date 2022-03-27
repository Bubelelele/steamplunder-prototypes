using UnityEngine;

public class ActiviationTrigger : MonoBehaviour
{
    [HideInInspector] public bool canSpin = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameManager.instance.player.gameObject)
        {
            canSpin = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == GameManager.instance.player.gameObject)
        {
            canSpin = false;
        }
    }
}
