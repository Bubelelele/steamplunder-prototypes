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
    public Transform swipeLeft, swipeRight, swipeBack;


    private bool chasePlayer = false;
    private bool rushPlayer = false;
    private bool canRush = false;
    
    //Waiting to attack
    private bool animationPlaying = false;
    private bool invokedOnce = false;
    private float  timeSinceLastAttack = 0;


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
        if (canRush)
        {
            timeSinceLastAttack += Time.deltaTime;
        }
        

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
                canRush = true;
            }
            else if (Vector3.Distance(player.transform.position, transform.position) > distanceToPlayerBeforeStop)
            {
                if (timeSinceLastAttack > 5f)
                {
                    enemyAnim.SetInteger("Swing", 1);
                    agent.acceleration = 60f;
                    agent.speed = 100f;
                    agent.SetDestination(player.transform.position);
                    animationPlaying = true;
                    pivot = false;
                    rushPlayer = true;
                }
                else
                {
                    agent.speed = movementSpeed;
                }

                agent.SetDestination(player.transform.position);
                distanceToPlayerBeforeStop = 3f;
                pivotDirChoosen = false;
            }
        }
        else if (!chasePlayer)
        {
            agent.speed = movementSpeed / 4;
            agent.SetDestination(homePoint);
        }
    }

    public override void EnemyInSight(){chasePlayer = true;}
    public override void EnemyOutOfSight(){chasePlayer = false;}
    public override void InAttackRange()
    {
        

        if (!animationPlaying)
        {
            if (!rushPlayer)
            {
                if (timeSinceLastAttack > 4.5f)
                {
                    Debug.Log("lol");
                    Attack();
                    agent.acceleration = 60;
                    animationPlaying = true;
                    pivot = false;
                }
                else if (timeSinceLastAttack <= 4.5f)
                {
                    agent.speed = movementSpeed / 4;
                    if (!invokedOnce)
                    {
                        Invoke("Attack", Random.Range(1.5f, 4.5f));
                        invokedOnce = true;
                    }
                    else if (Input.GetMouseButtonDown(InputManager.instance.AxeMouseBtn))
                    {
                        agent.speed = movementSpeed * 5;
                        pivot = false;
                        CancelInvoke();
                        Invoke("Attack", 0.3f);
                        side = Random.Range(0, 3);
                        animationPlaying = true;
                        if (side == 0) { agent.SetDestination(swipeLeft.position); }
                        else if (side == 1) { agent.SetDestination(swipeRight.position); }
                        else if (side == 2) { agent.SetDestination(swipeBack.position); }
                    }
                }
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
        
        agent.speed = movementSpeed;
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
        timeSinceLastAttack = 0;
        animationPlaying = false;
        invokedOnce = false;
        pivot = true;
        isStunned = false;
        rushPlayer = false;
        enemyAnim.SetInteger("Swing", 0);
        agent.SetDestination(transform.position);
        agent.speed = movementSpeed;
        agent.acceleration = 15;
    }
    public void ChangeSwordMat(Material newMat)
    {
        var tempMaterials = swordRenderer.materials;
        tempMaterials[2] = newMat;
        swordRenderer.materials = tempMaterials;
    }
}
