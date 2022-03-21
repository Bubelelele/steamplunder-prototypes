using UnityEngine;

public class PivotTrigger2 : MonoBehaviour
{
    [SerializeField] private Animator pivotAnim;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MovingBox" || other.gameObject.tag == "Player")
        {
            pivotAnim.SetBool("Pivot2", true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "MovingBox" || other.gameObject.tag == "Player")
        {
            pivotAnim.SetBool("Pivot2", false);
        }
    }
}