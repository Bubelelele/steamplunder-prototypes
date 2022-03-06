using UnityEngine;

public class BossDetectionTrigger : MonoBehaviour
{
    [HideInInspector] public bool canAttack;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == GameManager.instance.player.gameObject)
        {
            canAttack = true;
            Debug.Log("AttackNow");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == GameManager.instance.player.gameObject)
        {
            canAttack = false;
            Debug.Log("FollowPlayer");
        }
    }
}
