using UnityEngine;
using Random = UnityEngine.Random;

public class AttackScript : MonoBehaviour
{
    //Gun
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform muzzleTrans;
    [SerializeField] private int attackDamage = 50;

    [SerializeField] private GameObject detectionTrigger;
    [SerializeField] private GameObject bossCart;
    [SerializeField] private Animator bossAnim;


    private bool animationIsPlaying = false;
    private bool canShoot;

    private void Update()
    {
        float dist = Vector3.Distance(transform.position, GameManager.instance.player.gameObject.transform.position);
        
        if(dist > 6f && !animationIsPlaying && gameObject.GetComponent<BossStats>().isActive && canShoot)
        {
            bossCart.GetComponent<BossMovement>().DontWalkToPlayer();
            Shoot();
            Invoke("ShootCoolDown", 3f);
        }

        if (detectionTrigger.GetComponent<BossDetectionTrigger>().attackRange)
        {
            canShoot = true;
            if (!animationIsPlaying)
            {
                bossCart.GetComponent<BossMovement>().DontWalkToPlayer();

                int whichAttack = Random.Range(0, 4);

                if (whichAttack == 0)
                {
                    SlashSpree();
                }
                else if (whichAttack == 1)
                {
                    Block();
                }
                else if (whichAttack == 2)
                {
                    Punch();
                }
                else if (whichAttack == 3)
                {
                    SingleSlash();
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
        Invoke("Slash", Random.Range(2, 4f)*0.8f);
    }
    public void Slash()
    {
        bossAnim.SetTrigger("Slash");
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
        canShoot = false;
        bossAnim.SetTrigger("Shoot");
    }
    public void ShootCoolDown()
    {
        canShoot = true;
    }
    public void Stunned()
    {
        bossAnim.SetBool("Stunned", true);
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
    public void ActionOver()
    {

        animationIsPlaying = false;
        bossAnim.SetBool("Block", false);
        bossAnim.SetBool("Stunned", false);
        bossAnim.SetInteger("PunchInt", 0);
        bossCart.GetComponent<BossMovement>().WalkToPlayer();
    }

}
