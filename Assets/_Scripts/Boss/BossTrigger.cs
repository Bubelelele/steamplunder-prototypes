using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    [SerializeField] private GameObject boss;
    [SerializeField] private Animator gate;
    [SerializeField] private Animator secondStageDoor;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == GameManager.instance.player.gameObject)
        {
            Debug.Log("hey");
            boss.GetComponent<BossStats>().ActivateBoss();
            gate.SetBool("Entered", true);
            Destroy(gameObject);
        }

        if (other.gameObject == boss)
        {
            Debug.Log("activate");
            secondStageDoor.SetBool("OpenDoor", false);
        }
    }
}
