using System.Collections;
using UnityEngine;

public class CloseCombatEnemy : EnemyBase
{
    [HideInInspector] public bool lethal = false;
    [HideInInspector] public Vector3 targetPos;

    //Sword
    public Renderer swordRenderer;
    public Material swordMat, swordMatLight;

    //Movement
    public Transform swipeLeft, swipeRight;


    private bool chasePlayer = false;
    
    //Waiting to attack
    private bool animationPlaying = false;
    private bool invokedOnce = false;

    //Pivoting around the player
    private bool pivot = true;
    private int moveDir;
    private bool pivotDirChoosen = false;

    //Stunning
    private bool isStunned = false;
    private bool canBeStunned = false;
    private int side;
    private float distanceToPlayerBeforeStop = 3f;


    protected override void UpdateSense()
    {
        if (canBeStunned)
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!isStunned)
                {
                    lethal = false;
                    isStunned = true;
                    enemyAnim.SetTrigger("Stunned");
                    swordRenderer.materials[2] = swordMat;
                    agent.SetDestination(transform.position);
                }

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
            else if (Vector3.Distance(player.transform.position, transform.position) > distanceToPlayerBeforeStop)
            {
                agent.speed = movementSpeed;
                agent.SetDestination(player.transform.position);
                distanceToPlayerBeforeStop = 3f;
                pivotDirChoosen = false;
            }
        }
        else if (!chasePlayer)
        {
            agent.speed = movementSpeed / 2;
            agent.SetDestination(homePoint);
        }
    }

    public override void EnemyInSight(){chasePlayer = true;}
    public override void EnemyOutOfSight(){chasePlayer = false;}
    public override void InAttackRange()
    {
        if (!animationPlaying)
        {
            agent.speed = movementSpeed / 2;
            if (!invokedOnce)
            {
                Invoke("Attack", Random.Range(0.5f, 2.5f));
                invokedOnce = true;
            }
            else if (Input.GetMouseButtonDown(InputManager.instance.AxeMouseBtn))
            {
                agent.speed = movementSpeed*5;
                pivot = false;
                CancelInvoke();
                Invoke("Attack", 0.3f);
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
    
    public void CanBeStunned()
    {
        canBeStunned = true;
        ChangeSwordMat(swordMatLight);
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
