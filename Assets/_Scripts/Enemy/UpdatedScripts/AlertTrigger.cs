using UnityEngine;

public class AlertTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Enemy")
        {
            if(other.gameObject.GetComponent<HeavyEnemy>() != null)
            {
                other.gameObject.GetComponent<HeavyEnemy>().AwareOfPlayer();
            }
            else if (other.gameObject.GetComponent<RangedEnemy>() != null)
            {
                other.gameObject.GetComponent<RangedEnemy>().AwareOfPlayer();
            }
            else if (other.gameObject.GetComponent<CloseCombatEnemy>() != null)
            {
                other.gameObject.GetComponent<CloseCombatEnemy>().AwareOfPlayer();
            } 
        }
    }
}
