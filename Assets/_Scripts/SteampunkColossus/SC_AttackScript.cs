using UnityEngine;

public class SC_AttackScript : MonoBehaviour
{

    [HideInInspector] public int attackDamage;
    [HideInInspector] public bool leathal;
    [HideInInspector] public bool canAttack = false; 
    [HideInInspector] public bool animationPlaying = false; 

    [SerializeField] private GameObject bossCart;
    [SerializeField] private Animator bossAnim;

    [Header("Piston punch")]
    public bool pistonPunch;
    [SerializeField] private GameObject leftPistonPunch;
    [SerializeField] private GameObject leftHip;
    [SerializeField] private GameObject rightPistonPunch;
    [SerializeField] private GameObject rightHip;

    private void Update()
    {
        if (canAttack)
        {
            PistonPunch();

            if (pistonPunch)
            {
                attackDamage = 20;
                if (bossCart.GetComponent<SC_Movement>().playerOnLeftSide && !animationPlaying)
                {
                    animationPlaying = true;
                    bossAnim.SetBool("PunchLeft", true);
                    bossAnim.SetBool("PunchRight", false);
                    rightPistonPunch.SetActive(false);
                    rightHip.SetActive(true);
                    Invoke("Punch", 1f);

                }
                else if(!bossCart.GetComponent<SC_Movement>().playerOnLeftSide && !animationPlaying)
                {
                    animationPlaying = true;
                    bossAnim.SetBool("PunchLeft", false);
                    bossAnim.SetBool("PunchRight", true);
                    leftPistonPunch.SetActive(false);
                    leftHip.SetActive(true);
                    Invoke("Punch", 1f);
                }
            }
        }

    }
    public void CanAttack() { canAttack = true; }
    public void CannotAttack() { canAttack = false; }
    public void NotLeathal() { leathal = false; }
    public void IsLeathal()
    {
        leathal = true;
        AudioManager.instance.Play("swing");
    }
    public void PistonPunch()
    { 
        pistonPunch = true;
        bossCart.GetComponent<SC_Movement>().SetFOV(120f);
    }

    private void Punch()
    {
        bossAnim.SetTrigger("Punch");
        bossCart.GetComponent<SC_Movement>().DontLookAtPlayer();
    }

    public void LeftPunch()
    {
        leftPistonPunch.SetActive(true);
        leftPistonPunch.GetComponent<PistonPunch>().TurnOnPistonPunch();
        leftHip.SetActive(false);
    }
    public void RightPunch()
    {
        rightPistonPunch.SetActive(true);
        rightPistonPunch.GetComponent<PistonPunch>().TurnOnPistonPunch();
        rightHip.SetActive(false);
    }
    public void PistonPunchDone()
    {
        pistonPunch = false ;
        bossCart.GetComponent<SC_Movement>().SetFOV(80f);
        bossAnim.SetBool("PunchLeft", false);
        bossAnim.SetBool("PunchRight", false);

        leftHip.SetActive(true);
        leftPistonPunch.GetComponent<PistonPunch>().TurnOffPistonPunch();
        leftPistonPunch.SetActive(false);

        rightHip.SetActive(true);
        rightPistonPunch.GetComponent<PistonPunch>().TurnOffPistonPunch();
        rightPistonPunch.SetActive(false);
        animationPlaying = false;
        bossCart.GetComponent<SC_Movement>().LookAtPlayer();
    }

}
