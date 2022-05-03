using System.Collections;
using UnityEngine;

public class CloseCombatEnemy : EnemyBase
{
    [HideInInspector] public bool lethal = false;
    [HideInInspector] public Vector3 targetPos;

    public Transform swipeLeft, swipeRight;
    public Renderer swordRenderer;
    public Material swordMat, swordMatLight;

    private bool chasePlayer = false;
    private bool pivotDirChoosen = false;
    private int moveDir;
    private bool animationPlaying = false;
    private bool invokedOnce = false;
    private bool pivot = true;
    private bool isStunned = false;
    private bool canBeStunned = false;
    private bool blockStun = false;
    private int side;
    private float distanceToPlayerBeforeStop = 3f;




    public override void EnemyInSight(){chasePlayer = true;}
    public override void EnemyOutOfSight(){chasePlayer = false;}
    public override void InAttackRange()
    {
        if (!animationPlaying)
        {
            agent.speed = movementSpeed / 2;
            if (!invokedOnce)
            {
                Invoke("Attack", Random.Range(0.5f, 1.5f));
                invokedOnce = true;
            }
            else if (Input.GetMouseButtonDown(InputManager.instance.AxeMouseBtn))
            {
                Invoke("Attack", 0.3f);
                agent.speed = movementSpeed*5;
                pivot = false;
                CancelInvoke();
                side = Random.Range(0, 2);
                animationPlaying = true;
                if (side == 0) { agent.SetDestination(swipeLeft.position);}
                else if (side == 1) { agent.SetDestination(swipeRight.position);}
            }


            if (pivot)
            {
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
    }
    public override void Attack()
    {
            animationPlaying = true;
            enemyAnim.SetInteger("Swing", Random.Range(1, 4));
            agent.SetDestination(transform.position);
    }
    protected override void UpdateSense()
    {
        //Blocking
        if (canBeStunned)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!isStunned)
                {
                    lethal = false;
                    isStunned = true;
                    enemyAnim.SetTrigger("Stunned");
                    ChangeSwordMat(swordMat);
                    agent.SetDestination(transform.position);
                }

            }
        }
        else if (!canBeStunned)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                blockStun = true;
            }
        }

        if (chasePlayer && !animationPlaying)
        {
            if (Vector3.Distance(player.transform.position, transform.position) < distanceToPlayerBeforeStop)
            {
                agent.speed = movementSpeed;
                agent.SetDestination(transform.position);
                distanceToPlayerBeforeStop = 4f;
                InAttackRange();
            }
            else if(Vector3.Distance(player.transform.position, transform.position) > distanceToPlayerBeforeStop)
            {
                agent.speed = movementSpeed;
                agent.SetDestination(player.transform.position);
                distanceToPlayerBeforeStop = 3f;
                pivotDirChoosen = false;
            }
        }
        else if (!chasePlayer)
        {
            agent.speed = movementSpeed /2;
            agent.SetDestination(homePoint);
        }
    }
    public void CanBeStunned()
    {
        if (!blockStun)
        {
            canBeStunned = true;
            ChangeSwordMat(swordMatLight);
        }
        
    }
    public void CannnotBeStunned()
    {
        canBeStunned = false;
        ChangeSwordMat(swordMat);
    }
    public void Lethal()
    {
        lethal = true;
        
        agent.speed = movementSpeed / 2;
        agent.SetDestination(player.transform.position);
    }
    public void NotLethal()
    {
        lethal = false;
        agent.speed = movementSpeed / 10;
        agent.SetDestination(transform.position);
    }
    public void AnimationDone()
    {
        blockStun = false;
        animationPlaying = false;
        invokedOnce = false;
        pivot = true;
        isStunned = false;
        enemyAnim.SetInteger("Swing", 0);
        agent.SetDestination(transform.position);
    }
    public void ChangeSwordMat(Material newMat)
    {
        var tempMaterials = swordRenderer.materials;
        tempMaterials[2] = newMat;
        swordRenderer.materials = tempMaterials;
    }
}
