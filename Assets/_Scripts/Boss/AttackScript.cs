using UnityEngine;
using Random = UnityEngine.Random;

public class AttackScript : MonoBehaviour
{
    [HideInInspector] public int attackDamage;
    [HideInInspector] public bool leathal = false;

    //Gun
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform muzzleTrans;


    [SerializeField] private GameObject detectionTrigger;
    [SerializeField] private GameObject bossCart;
    [SerializeField] private Animator bossAnim;


    private bool animationIsPlaying = false;
    private bool canShoot = true;
    private bool isCharging = false;


    private void Update()
    {
        if (!animationIsPlaying)
        {

        }

        //Shooting
        float dist = Vector3.Distance(transform.position, GameManager.instance.player.gameObject.transform.position);
        
        if(dist > 6f && !animationIsPlaying && gameObject.GetComponent<BossStats>().isActive && canShoot)
        {
            bossCart.GetComponent<BossMovement>().DontWalkToPlayer();
            animationIsPlaying = true;
            Shoot();
            Invoke("ShootCoolDown", 5f);
        }


        //If within the range of the player
        if (detectionTrigger.GetComponent<BossDetectionTrigger>().attackRange)
        {
            if (isCharging)
            {
                attackDamage = 20;
                bossCart.GetComponent<BossMovement>().DontWalkToPlayer();
                bossCart.GetComponent<BossMovement>().DontLookAtPlayer();
                bossAnim.SetBool("Charge", false);
            }

            if (!animationIsPlaying)
            {
                bossCart.GetComponent<BossMovement>().DontWalkToPlayer();

                int whichAttack = Random.Range(0, 4);

                if (whichAttack == 0)
                {
                    SlashSpree();
                    attackDamage = 7;
                }
                else if (whichAttack == 1)
                {
                    Punch();
                    attackDamage = 12;
                }
                else if (whichAttack == 2)
                {
                    SingleSlash();
                    attackDamage = 8;
                }
                else
                {
                    Block();
                    attackDamage = 20;
                }
                animationIsPlaying = true;
            }
        }
    }
    public void SlashSpree()
    {
        bossAnim.SetTrigger("SlashSpree");
    }
    public void Block()
    {
        bossAnim.SetBool("Block", true);
        Invoke("Slash", Random.Range(30, 40f)*0.1f);
    }
    public void Slash()
    {
        bossAnim.SetBool("Block", false);
    }
    public void Punch()
    {
        int punchChance = Random.Range(0,3);

        if(punchChance == 0)
        {
            bossAnim.SetInteger("PunchInt", 1);
        }
        else if (punchChance == 1)
        {
            bossAnim.SetInteger("PunchInt", 2);
        }
        else
        {
            bossAnim.SetInteger("PunchInt", 3);
        }
    }
    public void SingleSlash()
    {
        bossAnim.SetTrigger("SingleSlash");
    }
    public void Shoot()
    {
        bossAnim.SetBool("Charge", true);
        canShoot = false;
        bossAnim.SetTrigger("Shoot");
    }
    public void ShootAudio()
    {
        AudioManager.instance.Play("gun");
    }
    public void ShootCoolDown()
    {
        canShoot = true;
    }
    public void ChargeSpeed()
    {
        isCharging = true;
        bossCart.GetComponent<BossMovement>().WalkToPlayer();
        bossCart.GetComponent<BossMovement>().ChargeSpeed();
    }
    public void Stunned()
    {
        bossAnim.SetBool("Stunned", true);
        bossCart.GetComponent<BossMovement>().DontLookAtPlayer();
    }
    public void InstanciateProjectile()
    {
        Quaternion projectileRotation = Quaternion.Euler(0f, muzzleTrans.rotation.eulerAngles.y, 0f);
        ProjectileFromBoss projectile = Instantiate(projectilePrefab, muzzleTrans.position, projectileRotation).GetComponent<ProjectileFromBoss>();
        projectile.damageAmount = attackDamage;
    }
    public void Shield()
    {
        gameObject.GetComponent<BossStats>().CannotBeHarmed();
    }
    public void NoShield()
    {
        bossAnim.SetBool("Block", false);
        gameObject.GetComponent<BossStats>().CanBeHarmed();
        
    }

    public void IsLeathal()
    {
        leathal = true;
        AudioManager.instance.Play("swing");
    }
    public void NotLeathal()
    {
        leathal = false;
    }
    public void ActionOver()
    {

        animationIsPlaying = false;
        isCharging = false;
        bossAnim.SetBool("Block", false);
        bossAnim.SetBool("Stunned", false);
        bossAnim.SetInteger("PunchInt", 0);
        bossCart.GetComponent<BossMovement>().WalkToPlayer();
        bossCart.GetComponent<BossMovement>().LookAtPlayer();
        bossCart.GetComponent<BossMovement>().NormalSpeed();
    }

}
