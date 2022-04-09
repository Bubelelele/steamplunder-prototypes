using UnityEngine;

public class SC_AttackScript : MonoBehaviour
{

    [HideInInspector] public int attackDamage;
    [HideInInspector] public bool leathal;
    [HideInInspector] public bool canAttack = false; 
    [HideInInspector] public bool animationPlaying = false; 

    [SerializeField] private GameObject bossCart;
    [SerializeField] private GameObject player;
    [SerializeField] private Animator bossAnim;

    [Header("Swipe")]
    [HideInInspector] public bool swipe;
    public int swipeDamage = 15;

    [Header("Slam")]
    [HideInInspector] public bool slam;
    public int slamDamage = 6;

    [Header("Piston punch")]
    [HideInInspector] public bool pistonPunch;
    public int punchDamage = 20;

    [SerializeField] private GameObject leftPistonPunch;
    [SerializeField] private GameObject leftHip;
    [SerializeField] private GameObject rightPistonPunch;
    [SerializeField] private GameObject rightHip;

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
                int attackID = Random.Range(0, 2);

                if (attackID == 1)
                {
                    PistonPunch();
                }
                else
                {
                    Swipe();
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
    public void Swipe() 
    {
        animationPlaying = true;
        attackDamage = swipeDamage;
        bossAnim.SetTrigger("Swipe");
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
        
        //Slam
        bossAnim.SetBool("SlamLeft", false);
        bossAnim.SetBool("SlamRight", false);
        slam = false;

        //Punch
        bossAnim.SetBool("PunchLeft", false);
        bossAnim.SetBool("PunchRight", false);

    }

}
