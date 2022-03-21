using UnityEngine;

public class PivotTrigger : MonoBehaviour
{
    [SerializeField] private Animator pivotAnim;
    [SerializeField] private Animator someOtherAnimationsAnim;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MovingBox" || other.gameObject.tag == "Player")
        {
            pivotAnim.SetBool("Pivot1", true);
            someOtherAnimationsAnim.SetBool("SA1", true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "MovingBox" || other.gameObject.tag == "Player")
        {
            pivotAnim.SetBool("Pivot1", false);
            someOtherAnimationsAnim.SetBool("SA1", false);
        }
    }
}
