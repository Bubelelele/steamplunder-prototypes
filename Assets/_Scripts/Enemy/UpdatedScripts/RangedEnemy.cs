using UnityEngine;

public class RangedEnemy : EnemyBase
{
    [Header("Ranged parameters")]
    public float bulletSpeed;

    [Header("References")]
    public GameObject bulletPrefab;
    public Transform muzzle;

    private float distanceAttack = 5f;
    private float distanceRun;
    protected override void UpdateSense()
    {
        distanceRun = distanceAttack - 1.5f;

        //Moving towards the player
        if (chasePlayer && !animationPlaying)
        {
            agent.SetDestination(player.transform.position);
            if (Vector3.Distance(player.transform.position, transform.position) < distanceAttack)
            {
                InAttackRange();
                StopMovingToDestination();
            }
            else if (Vector3.Distance(player.transform.position, transform.position) > distanceRun && !idle)
            {
                CanMoveToDestination(movementSpeed);
            }
        }
        else if (!chasePlayer && !animationPlaying)
        {
            agent.speed = slowWalkingSpeed;
            agent.SetDestination(homePoint);
        }

        if (inAttackRange)
        {
            Attack();
        }
    }
    public override void Attack()
    {
        animationPlaying = true;
        enemyAnim.SetTrigger("Shoot");
    }
    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
        bullet.GetComponent<Bullet>().SetDamage(attackDamage);
        bullet.GetComponent<Bullet>().SetBulletSpeed(bulletSpeed);
    }
    private void AnimationDone()
    {
        animationPlaying = false;
    }
}
