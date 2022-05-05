using UnityEngine;

public class HeavyEnemy : EnemyBase
{
    private float distanceAttack = 3f;
    private float distanceChase;

    protected override void UpdateSense()
    {
        distanceChase = distanceAttack + 1.5f;

        //Moving towards the player
        if (chasePlayer && !animationPlaying)
        {
            agent.SetDestination(player.transform.position);
            if (Vector3.Distance(player.transform.position, transform.position) < distanceAttack)
            {
                enemyAnim.SetBool("Block", true);
                InAttackRange();
                ChangeSpeed(slowWalkingSpeed);
            }
            else if (Vector3.Distance(player.transform.position, transform.position) > distanceChase && !idle)
            {
                enemyAnim.SetBool("Block", false);
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
        
        //animationPlaying = true;

    }
}
