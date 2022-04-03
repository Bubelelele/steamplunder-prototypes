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
}
