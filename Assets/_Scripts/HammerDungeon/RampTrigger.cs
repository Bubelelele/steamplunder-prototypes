using UnityEngine;

public class RampTrigger : MonoBehaviour
{
    public Animator rampAnim;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MovingBox")
        {
            Debug.Log("Yes");
            other.transform.position = transform.position;
            other.transform.parent = transform;

            other.transform.gameObject.GetComponent<WoodenBox>().enabled = false;
            Invoke("LowerRamp", 1f);
            

        }
    }
    private void LowerRamp()
    {
        rampAnim.SetBool("RampDown", true);
    }
}
