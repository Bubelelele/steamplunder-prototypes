using UnityEngine;
using Random = UnityEngine.Random;

public class AttackScript : MonoBehaviour
{
    [HideInInspector] public int attackDamage;
    [HideInInspector] public bool leathal = false;
    [HideInInspector] public bool lastStage = false;

    //Gun
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject gearBoomerang;
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

            if (dist > 6f && !animationIsPlaying && canShoot)
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
                    attackDamage = 20;
                }
            }


            //If within the range of the player
            if (detectionTrigger.GetComponent<BossDetectionTrigger>().attackRange)
            {
                if (isCharging)
                {
                    //Charge at the player
                    attackDamage = 20;
                    bossCart.GetComponent<BossMovement>().DontWalkToPlayer();
                    bossCart.GetComponent<BossMovement>().DontLookAtPlayer();
                    bossAnim.SetBool("Charge", false);
                }

                if (!animationIsPlaying)
                {
                    int whichAttack = Random.Range(0, 6);

                    if (whichAttack == 0) //Slash three times
                    {
                        bossCart.GetComponent<BossMovement>().SwordSwingSpeed();
                        bossAnim.SetTrigger("SlashSpree");
                        attackDamage = 7;
                    }
                    else if (whichAttack == 1) //Single slash
                    {
                        bossCart.GetComponent<BossMovement>().SwordSwingSpeed();
                        bossAnim.SetTrigger("SingleSlash");
                        attackDamage = 8;
                    }
                    else if (whichAttack == 2 || whichAttack == 3) 
                    {
                        if (!lastStage) // Punch
                        {
                            Punch();
                            attackDamage = 12;
                        }
                        else // Gearpunch
                        {
                            GearPunch();
                            attackDamage = 16;
                        }        
                        bossCart.GetComponent<BossMovement>().DontWalkToPlayer();
                    }
                    else
                    {
                        if (!lastStage) // Block
                        {
                            bossAnim.SetBool("Block", true);
                            Invoke("Slash", Random.Range(30, 40f) * 0.1f);
                            attackDamage = 20;
                        }
                        else // Gearattack
                        {
                            bossAnim.SetTrigger("GearAttack");
                            attackDamage = 20;
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
        bossAnim.SetBool("Stunned", true);
        bossCart.GetComponent<BossMovement>().DontLookAtPlayer();
    }


    //Functions called from animations
    public void ShootAudio(){   AudioManager.instance.Play("gun");}
    public void Shield() { gameObject.GetComponent<BossStats>().CannotBeHarmed(); }
    public void NoShield()
    {
        bossAnim.SetBool("Block", false);
        gameObject.GetComponent<BossStats>().CanBeHarmed();

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

        animationIsPlaying = false;
        isCharging = false;
        bossAnim.SetBool("Block", false);
        bossAnim.SetBool("Stunned", false);
        bossAnim.SetInteger("PunchInt", 0);
        bossAnim.SetInteger("GearPunchInt", 0);
        bossAnim.SetBool("Abort", false);
        bossCart.GetComponent<BossMovement>().WalkToPlayer();
        bossCart.GetComponent<BossMovement>().LookAtPlayer();
        bossCart.GetComponent<BossMovement>().NormalSpeed();
    }

}
