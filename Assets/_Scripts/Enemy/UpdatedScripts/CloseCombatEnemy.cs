using System.Collections;
using UnityEngine;

public class CloseCombatEnemy : EnemyBase
{
    private bool chasePlayer = false;
    private bool pivotDirChoosen = false;
    private int moveDir;
    private bool animationPlaying = false;
    private float distanceToPlayerBeforeStop = 2.5f;

    

    public override void EnemyInSight(){chasePlayer = true;}
    public override void EnemyOutOfSight(){chasePlayer = false;}
    public override void InAttackRange()
    {
        
        if (!animationPlaying)
        {
            Invoke("Attack", Random.Range(2f, 3f));
            agent.speed = movementSpeed / 10;

            if (!pivotDirChoosen)
            {
                moveDir = Random.Range(0, 2);
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
    public override void Attack()
    {
        Debug.Log("nop");
        animationPlaying = true;
        enemyAnim.SetInteger("Swing", Random.Range(1, 4));
        agent.SetDestination(transform.position);
    }
    protected override void UpdateSense()
    {
        if (chasePlayer && !animationPlaying)
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
    public void AnimationDone()
    {
        animationPlaying = false;
        enemyAnim.SetInteger("Swing", 0);
        agent.SetDestination(transform.position);
    }
}
