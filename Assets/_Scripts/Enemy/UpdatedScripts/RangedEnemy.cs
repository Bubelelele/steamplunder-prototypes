using UnityEngine;

public class RangedEnemy : EnemyBase
{
    [Header("Ranged parameters")]
    public float bulletSpeed;
    public float backingUpSpeed;

    [Header("References")]
    public GameObject bulletPrefab;
    public Transform muzzle;

    private float distanceAttack = 7f;
    private float distanceRunAway;
    private float distanceRunTowards;
    private bool isShooting;
    protected override void UpdateSense()
    {
        distanceRunAway = distanceAttack - 2.5f;
        distanceRunTowards = distanceAttack + 1.5f;

        //Moving towards the player
        if (chasePlayer && !animationPlaying)
        {
            agent.SetDestination(player.transform.position);
            if (Vector3.Distance(player.transform.position, transform.position) <= distanceRunTowards && Vector3.Distance(player.transform.position, transform.position) >= distanceRunAway)
            {
                InAttackRange();
                animationPlaying = true;
            }
            else if (Vector3.Distance(player.transform.position, transform.position) > distanceRunTowards && !idle)
            {
                CanMoveToDestination(movementSpeed);
            }
            else if (Vector3.Distance(player.transform.position, transform.position) < distanceRunAway && !idle)
            {
                StopMovingToDestination();
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, transform.position - transform.forward, backingUpSpeed * Time.deltaTime);
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
        if (isShooting && Vector3.Distance(player.transform.position, transform.position) < distanceRunAway)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, transform.position - transform.forward, slowWalkingSpeed * Time.deltaTime);
        }
    }
    public override void Attack()
    {
        enemyAnim.SetBool("Shoot", true);
        StopMovingToDestination();
        isShooting = true;
    }
    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
        bullet.GetComponent<Bullet>().SetDamage(attackDamage);
        bullet.GetComponent<Bullet>().SetBulletSpeed(bulletSpeed);
    }
    private void AnimationDone()
    {
        enemyAnim.SetBool("Shoot", false);
        isShooting = false;
        CanMoveToDestination(movementSpeed);
        animationPlaying = false;
    }
}
