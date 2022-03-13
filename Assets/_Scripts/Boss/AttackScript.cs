using UnityEngine;
using Random = UnityEngine.Random;

public class AttackScript : MonoBehaviour
{
    [HideInInspector] public int attackDamage;
    [HideInInspector] public bool leathal = false;
    [HideInInspector] public bool lastStage = false;
    [HideInInspector] public bool pushBack = false;

    //Gun
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject gearBoomerang;
    [SerializeField] private GameObject frontOffset;
    [SerializeField] private Transform muzzleTrans;
    

    [SerializeField] private GameObject gauntlet;
    [SerializeField] private GameObject gearGauntlet;
    [SerializeField] private GameObject boomerangStuff;


    [SerializeField] private GameObject detectionTrigger;
    [SerializeField] private GameObject bossCart;
    [SerializeField] private Animator bossAnim;


    private bool animationIsPlaying = false;
    private bool canShoot = true;
    private bool isCharging = false;
    private bool canBeStunned = false;



    private void Update()
    {
        if (gameObject.GetComponent<BossStats>().isActive)
        {
            if (lastStage)
            {
                gauntlet.SetActive(false);
                gearGauntlet.SetActive(true);
                boomerangStuff.SetActive(true);
            }
            //Shooting and charging
            float dist = Vector3.Distance(transform.position, GameManager.instance.player.gameObject.transform.position);

            if (dist > 8f && !animationIsPlaying && canShoot)
            {
                bossCart.GetComponent<BossMovement>().DontWalkToPlayer();
                animationIsPlaying = true;
                if (!lastStage)
                {
                    //Shooting
                    canShoot = false;
                    bossAnim.SetBool("Charge", true);
                    bossAnim.SetTrigger("Shoot");
                    Invoke("ShootCoolDown", 5f);
                }
                else
                {
                    //Shoot gear
                    bossAnim.SetTrigger("ShootGear");
                    bossAnim.SetBool("Charge", true);
                    attackDamage = 7;
                    pushBack = false;
                }
            }


            //If within the range of the player
            if (detectionTrigger.GetComponent<BossDetectionTrigger>().attackRange)
            {
                if (isCharging)
                {
                    //Charge at the player
                    attackDamage = 10;
                    bossCart.GetComponent<BossMovement>().DontWalkToPlayer();
                    bossCart.GetComponent<BossMovement>().DontLookAtPlayer();
                    bossAnim.SetBool("Charge", false);
                    pushBack = true;
                }

                if (!animationIsPlaying)
                {
                    int whichAttack = Random.Range(0, 6);

                    if (whichAttack == 0) //Slash three times
                    {
                        bossCart.GetComponent<BossMovement>().SwordSwingSpeed();
                        bossAnim.SetTrigger("SlashSpree");
                        attackDamage = 7;
                        pushBack = true;
                    }
                    else if (whichAttack == 1) //Single slash
                    {
                        bossCart.GetComponent<BossMovement>().SwordSwingSpeed();
                        bossAnim.SetTrigger("SingleSlash");
                        attackDamage = 7;
                        pushBack = true;
                    }
                    else if (whichAttack == 2 || whichAttack == 3) 
                    {
                        if (!lastStage) // Punch
                        {
                            Punch();
                            attackDamage = 5;
                            pushBack = false;
                        }
                        else // Gearpunch
                        {
                            GearPunch();
                            pushBack = false;
                            attackDamage = 7;
                        }        
                        bossCart.GetComponent<BossMovement>().DontWalkToPlayer();
                    }
                    else
                    {
                        if (!lastStage) // Block
                        {
                            bossAnim.SetBool("Block", true);
                            Invoke("Slash", Random.Range(30, 40f) * 0.1f);
                            attackDamage = 10;
                            
                        }
                        else // Gearattack
                        {
                            bossAnim.SetTrigger("GearAttack");
                            attackDamage = 13;
                            pushBack = true;
                            bossCart.GetComponent<BossMovement>().DontWalkToPlayer();
                        }
                        
                    }
                    animationIsPlaying = true;
                }
            }
        }
        
    }

    //Functions called from this script
    private void ShootCoolDown() { canShoot = true; }
    private void Slash()
    {
        bossCart.GetComponent<BossMovement>().SwordSwingSpeed();
        bossAnim.SetBool("Block", false);
        pushBack = true;
    }
    private void Punch()
    {
        //Randomize amount of punches
        int punchChance = Random.Range(0,3);

        if(punchChance == 0){   bossAnim.SetInteger("PunchInt", 1);}
        else if (punchChance == 1){    bossAnim.SetInteger("PunchInt", 2);}
        else{   bossAnim.SetInteger("PunchInt", 3);}
    }
    private void GearPunch()
    {
        //Randomize amount of punches
        int punchChance = Random.Range(0, 3);

        if (punchChance == 0) { bossAnim.SetInteger("GearPunchInt", 1); }
        else if (punchChance == 1) { bossAnim.SetInteger("GearPunchInt", 2); }
        else { bossAnim.SetInteger("GearPunchInt", 3); }
    }



    //Functions called from other scripts
    public void Abort() { bossAnim.SetBool("Abort", true); }
    public void LastStage() { lastStage = true; }
    public void Stunned()
    {
        if (canBeStunned)
        {
            bossAnim.SetBool("Stunned", true);
            bossCart.GetComponent<BossMovement>().DontLookAtPlayer();
        }

    }


    //Functions called from animations
    public void ShootAudio(){   AudioManager.instance.Play("gun");}
    public void Shield() 
    {
        canBeStunned = true;
        gameObject.GetComponent<BossStats>().CannotBeHarmed(); 
    }
    public void NoShield()
    {
        bossAnim.SetBool("Block", false);
        gameObject.GetComponent<BossStats>().CanBeHarmed();
        frontOffset.SetActive(false);

    }
    public void NotLeathal() { leathal = false; }
    public void IsLeathal()
    {
        leathal = true;
        AudioManager.instance.Play("swing");
    }
    public void ChargeSpeed()
    {
        isCharging = true;
        bossCart.GetComponent<BossMovement>().WalkToPlayer();
        bossCart.GetComponent<BossMovement>().ChargeSpeed();
    }
    public void InstanciateProjectile()
    {
        Quaternion projectileRotation = Quaternion.Euler(0f, muzzleTrans.rotation.eulerAngles.y, 0f);
        ProjectileFromBoss projectile = Instantiate(projectilePrefab, muzzleTrans.position, projectileRotation).GetComponent<ProjectileFromBoss>();
        projectile.damageAmount = attackDamage;
    }
    public void ShootGear()
    {
        gearBoomerang.GetComponent<GearBoomerang>().ActivateBoomerang();
    }
    public void ActionOver()
    {
        Invoke("AnimationDelay", 0f);
        bossAnim.SetInteger("PunchInt", 0);
        bossAnim.SetInteger("GearPunchInt", 0);
        isCharging = false;
        canBeStunned = false;
        bossAnim.SetBool("Block", false);
        bossAnim.SetBool("Stunned", false);
        bossAnim.SetBool("Abort", false);
        bossCart.GetComponent<BossMovement>().WalkToPlayer();
        bossCart.GetComponent<BossMovement>().LookAtPlayer();
        bossCart.GetComponent<BossMovement>().NormalSpeed();
        frontOffset.SetActive(true);
        pushBack = false;
    }
    public void AnimationDelay()
    {
        animationIsPlaying = false;
    }

}
