using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public GameObject bossBody;
    public GameObject bossCart;
    public GameObject r_dragOffFoot;
    public GameObject l_dragOffFoot;

    public GameObject projectilePrefab;
    public Transform muzzleTrans;


    //Boss body
    private void ActivateBoss()
    {
        bossBody.GetComponent<SC_Stats>().ActivateBoss();

    }
    private void DeactivateBoss()
    {
        bossBody.GetComponent<SC_Stats>().DeactivateBoss();

    }
    private void Leathal()
    {
        bossBody.GetComponent<SC_AttackScript>().IsLeathal();
    }
    private void NotLeathal()
    {
        bossBody.GetComponent<SC_AttackScript>().NotLeathal();
    }
    private void LeftPunch()
    {
        bossBody.GetComponent<SC_AttackScript>().LeftPunch();
    }
    private void RightPunch()
    {
        bossBody.GetComponent<SC_AttackScript>().RightPunch();
    }
    private void PistonPunchDone()
    {
        bossBody.GetComponent<SC_AttackScript>().PistonPunchDone();
    }
    private void Shoot()
    {
        Instantiate(projectilePrefab, muzzleTrans.position, muzzleTrans.rotation);
    }
    private void AnimationDone()
    {
        bossBody.GetComponent<SC_AttackScript>().AnimationDone();
    }
    private void InvokeRaiseUp()
    {
        Invoke("RaiseUp", 3);
        r_dragOffFoot.SetActive(true);
        l_dragOffFoot.SetActive(true);

    }
    private void RaiseUp()
    {
        r_dragOffFoot.SetActive(false);
        l_dragOffFoot.SetActive(false);
        gameObject.GetComponent<Animator>().SetBool("IsDown", false);
    }


    //Boss cart
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



    
