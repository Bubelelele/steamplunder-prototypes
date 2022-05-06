using UnityEngine;

public class HeavyEnemy : EnemyBase
{
    [HideInInspector] public bool lethal = false;

    private float distanceAttack = 3f;
    private float distanceChase;
    private bool canStun;
    private bool attackInvoked;

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
            if (!attackInvoked)
            {
                Invoke("Attack", Random.Range(0.1f, 1f));
                attackInvoked = true;
            }
        }

    }
    public override void Attack()
    {
        enemyAnim.SetInteger("Punch", Random.Range(1, 3));
        animationPlaying = true;

    }
    public void Stun()
    {
        if (canStun)
        {
            NotLethal();
            rotationSpeed = 0;
            enemyAnim.SetBool("Fall", true);
            Invoke("RiseUp", Random.Range(0.1f, 1.5f));
            animationPlaying = true;
        }
    }

    private void RiseUp()
    {
        enemyAnim.SetBool("Fall", false);
    }
    private void CanStun()
    {
        canStun = true;
        GetComponent<EnemyStats>().CannotBeHarmed();
    }
    private void CannotStun()
    {
        canStun = false;
        GetComponent<EnemyStats>().CanBeHarmed();
    }
    public void Lethal()
    {
        lethal = true;
    }
    public void NotLethal()
    {
        lethal = false;
    }
    private void AnimationDone()
    {
        enemyAnim.SetInteger("Punch", 0);
        animationPlaying = false;
        rotationSpeed = 10;
        attackInvoked = false;
    }

}
