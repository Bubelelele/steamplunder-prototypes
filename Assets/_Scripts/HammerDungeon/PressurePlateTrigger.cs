using UnityEngine;

public class PressurePlateTrigger : MonoBehaviour
{
    public Animator liftAnim;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "MovingBox" || other.gameObject.tag == "Player")
        {
            liftAnim.SetBool("LiftUp", true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "MovingBox" || other.gameObject.tag == "Player")
        {
            liftAnim.SetBool("LiftUp", false);
        }
    }
}
