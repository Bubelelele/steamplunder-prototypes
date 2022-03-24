using UnityEngine;

public class QuickFixFinal : MonoBehaviour
{
    [SerializeField] private Animator barrierAnim;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "MetalBox")
        {
            barrierAnim.SetBool("BarrierDown", true);
        }
        if (other.gameObject.name == "WoodenBox")
        {
            barrierAnim.SetBool("BarrierDown", false);
        }
    }
}
