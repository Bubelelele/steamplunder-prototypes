using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public GameObject bossBody;
    public GameObject bossCart;
    public GameObject bossCanvas;
    public GameObject cogThatShootsOff;
    public GameObject gun;


    //Boss body
    private void ActivateBoss()
    {
        bossBody.GetComponent<SC_Stats>().ActivateBoss();
        bossCanvas.SetActive(true);
    }
    private void DeactivateBoss()
    {
        bossBody.GetComponent<SC_Stats>().DeactivateBoss();
        bossCanvas.SetActive(false);
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
    private void AnimationDone()
    {
        bossBody.GetComponent<SC_AttackScript>().AnimationDone();
    }
    private void CogThatShootsOff()
    {
        cogThatShootsOff.transform.parent = null;
        cogThatShootsOff.AddComponent<BoxCollider>();
        cogThatShootsOff.AddComponent<Rigidbody>();
        cogThatShootsOff.GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * 1f, ForceMode.Impulse);
        Invoke("Hide", 2f);
    }

    private void Hide()
    {
        Destroy(cogThatShootsOff);
    }
    private void SecondPhaseOff()
    {
        gameObject.GetComponent<Animator>().SetBool("SecondPhase", false);
        gun.GetComponent<Hotdog_Gun>().GunActive();
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



    
