using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public GameObject bossBody;
    public GameObject bossCart;

    private void ActivateBoss()
    {
        bossBody.GetComponent<SC_Stats>().ActivateBoss();
    }
    private void DeactivateBoss()
    {
        bossBody.GetComponent<SC_Stats>().DeactivateBoss();
    }
    private void Step()
    {
        bossCart.GetComponent<SC_Movement>().Step();
    }
    private void NoStep()
    {
        bossCart.GetComponent<SC_Movement>().NoStep();
    }
    private void Spin()
    {
        bossCart.GetComponent<SC_Movement>().Spin();
    }
    private void NoSpin()
    {
        bossCart.GetComponent<SC_Movement>().NoSpin();
    }
}
