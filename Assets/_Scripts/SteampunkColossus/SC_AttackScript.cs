using UnityEngine;

public class SC_AttackScript : MonoBehaviour
{

    [HideInInspector] public int attackDamage;
    [HideInInspector] public bool leathal;
    [HideInInspector] public bool canAttack = false; 
    [HideInInspector] public bool animationPlaying = false;
    [HideInInspector] public int footOff = 0;

    [Header("Swipe")]
    [HideInInspector] public bool swipe;
    public int swipeDamage = 15;

    [Header("Slam")]
    [HideInInspector] public bool slam;
    public int slamDamage = 6;

    [Header("Piston punch")]
    [HideInInspector] public bool pistonPunch;
    public int punchDamage = 20;

    [SerializeField] private GameObject bossCart;
    [SerializeField] private GameObject player;
    [SerializeField] private Animator bossAnim;

    [SerializeField] private GameObject leftPistonPunch;
    [SerializeField] private GameObject leftHip;
    [SerializeField] private GameObject rightPistonPunch;
    [SerializeField] private GameObject rightHip;

    private bool attackIDChecked = false;


    private void Update()
    {
        if (canAttack)
        {
            if (slam)
            {
                Slam();                
            }
            else
            {
                int bottom = 0;
                int attackRange = 2;
                int attackID = 10;
                if (gameObject.GetComponent<SC_Stats>().secondPhaseDone)
                {
                    attackRange = 4;
                }
                if (footOff == 1)
                {
                    bottom = 2;
                }
                else if (footOff == 2)
                {
                    canAttack = false;
                }
                if (!attackIDChecked)
                {
                    attackID = Random.Range(bottom, attackRange);
                    attackIDChecked = true;
                }


                if (attackID == 0)
                {
                    PistonPunch();
                }
                else if(attackID == 1)
                {
                    Swipe();
                }
                else if (attackID == 2 || attackID == 3)
                {
                    Shoot();
                }

            }
        }

    }
    public void CanAttack() { canAttack = true; }
    public void CannotAttack() { canAttack = false; }
    public void NotLeathal() { leathal = false; }
    public void FootOff() { footOff++; }
    public void IsLeathal()
    {
        leathal = true;
        AudioManager.instance.Play("swing");
    }
    public void Swipe() 
    {
        animationPlaying = true;
        attackDamage = swipeDamage;
        bossAnim.SetBool("Swipe", true);
    }
    public void Shoot()
    {
        bossAnim.SetBool("Shoot", true);
        animationPlaying = true;
    }
    public void CanSlam(){ slam = true; }
    public void Slam()
    {
        if (bossCart.GetComponent<SC_Movement>().playerOnLeftSide)
        {
            attackDamage = slamDamage;
            animationPlaying = true;
            bossAnim.SetBool("SlamLeft", true);
        }
        else if (!bossCart.GetComponent<SC_Movement>().playerOnLeftSide)
        {
            attackDamage = slamDamage;
            animationPlaying = true;
            bossAnim.SetBool("SlamRight", true);
        }
    }

    public void PistonPunch()
    {
        if (bossCart.GetComponent<SC_Movement>().playerOnLeftSide && !animationPlaying)
        {
            animationPlaying = true;
            attackDamage = punchDamage;
            bossAnim.SetBool("PunchLeft", true);
            bossAnim.SetBool("PunchRight", false);
            rightPistonPunch.SetActive(false);
            rightHip.SetActive(true);
            Invoke("Punch", 1f);

        }
        else if (!bossCart.GetComponent<SC_Movement>().playerOnLeftSide && !animationPlaying)
        {
            animationPlaying = true;
            attackDamage = punchDamage;
            bossAnim.SetBool("PunchLeft", false);
            bossAnim.SetBool("PunchRight", true);
            leftPistonPunch.SetActive(false);
            leftHip.SetActive(true);
            Invoke("Punch", 1f);
        }
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
        bossCart.GetComponent<SC_Movement>().SetFOV(80f);


        leftHip.SetActive(true);
        leftPistonPunch.GetComponent<PistonPunch>().TurnOffPistonPunch();
        leftPistonPunch.SetActive(false);

        rightHip.SetActive(true);
        rightPistonPunch.GetComponent<PistonPunch>().TurnOffPistonPunch();
        rightPistonPunch.SetActive(false);
        AnimationDone();
        bossCart.GetComponent<SC_Movement>().LookAtPlayer();
    }
    public void AnimationDone()
    {
        animationPlaying = false;
        attackIDChecked = false;

        bossAnim.SetBool("Swipe", false); //Swipe

        bossAnim.SetBool("SlamLeft", false); //Slam
        bossAnim.SetBool("SlamRight", false);
        slam = false;

        bossAnim.SetBool("PunchLeft", false); //Punch
        bossAnim.SetBool("PunchRight", false);
    }

}
