using UnityEngine;

public class CloseCombatEnemy : EnemyBase
{
    private bool chasePlayer = false;
    private bool pivotDirChoosen = false;
    private int moveDir;
    private float distanceToPlayerBeforeStop = 2.5f;

    private bool attacking = false;

    public override void EnemyInSight()
    {
        chasePlayer = true;
    }
    public override void EnemyOutOfSight()
    {
        chasePlayer = false;
    }
    public override void InAttackRange()
    {
        if (!attacking)
        {
            agent.speed = movementSpeed / 5;

            if (!pivotDirChoosen)
            {
                moveDir = Random.Range(0, 3);
                pivotDirChoosen = true;
            }
            if (moveDir == 0)
            {
                agent.SetDestination(transform.position + Vector3.Cross(player.transform.position - transform.position, Vector3.up));
            }
            else if (moveDir == 1)
            {
                agent.SetDestination(transform.position + Vector3.Cross(player.transform.position - transform.position, -Vector3.up));
            }
        }
    }
    protected override void UpdateSense()
    {
        if (chasePlayer)
        {
            if (Vector3.Distance(player.transform.position, transform.position) < distanceToPlayerBeforeStop)
            {
                agent.speed = movementSpeed;
                agent.SetDestination(transform.position);
                distanceToPlayerBeforeStop = 3f;
                InAttackRange();
            }
            else if(Vector3.Distance(player.transform.position, transform.position) > distanceToPlayerBeforeStop)
            {
                agent.speed = movementSpeed;
                agent.SetDestination(player.transform.position);
                distanceToPlayerBeforeStop = 2.5f;
                pivotDirChoosen = false;
            }
        }
        else if (!chasePlayer)
        {
            agent.speed = movementSpeed /2;
            agent.SetDestination(homePoint);
        }
    }
}
