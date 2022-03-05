using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    [SerializeField] private GameObject boss;
    [SerializeField] private Animator gate;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == GameManager.instance.player.gameObject)
        {
            boss.GetComponent<BossStats>().ActivateBoss();
            gate.SetBool("Entered", true);
        }
    }
}
