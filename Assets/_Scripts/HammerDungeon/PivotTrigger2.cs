using UnityEngine;

public class PivotTrigger2 : MonoBehaviour
{
    [SerializeField] private Animator pivotAnim;
    [SerializeField] private Animator someOtherAnimationsAnim;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MovingBox" || other.gameObject.tag == "Player")
        {
            pivotAnim.SetBool("Pivot2", true);
            someOtherAnimationsAnim.SetBool("SA2", true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "MovingBox" || other.gameObject.tag == "Player")
        {
            pivotAnim.SetBool("Pivot2", false);
            someOtherAnimationsAnim.SetBool("SA2", false);
        }
    }
}