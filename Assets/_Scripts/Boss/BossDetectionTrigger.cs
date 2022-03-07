using UnityEngine;

public class BossDetectionTrigger : MonoBehaviour
{
    [HideInInspector] public bool attackRange;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == GameManager.instance.player.gameObject)
        {
            attackRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == GameManager.instance.player.gameObject)
        {
            attackRange = false;
        }
    }
}
