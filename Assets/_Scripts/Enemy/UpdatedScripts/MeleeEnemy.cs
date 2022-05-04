using UnityEngine;

public class MeleeEnemy : EnemyBase
{
    //Paramaters to other scripts
    [HideInInspector] public bool lethal = false;
    [HideInInspector] public Vector3 targetPos;

    //Other
    

    private bool inAttackRange;

    //Idle
    public float idleTurn = 50f;
    public bool patrol;
    public Transform[] patrolDestinations;

    private int numberOfMoves = 0;
    private bool moveToPatrolDestination = false;
    private bool canIdleTurn = false;
    private bool idleTurnedDone = true;

    //Sword
    public Renderer swordRenderer;
    public Material swordMat, swordMatLight;

    //Movement
    public Transform pivotTrans;
    public float distanceAttack = 3f;
    public float pivotSpeed = 20f;
    public float backAndForwardSpeed = 2f;
    public float stepBackSpeed = 5f;
    public float slowWalkingSpeed = 5f;

    public Transform zigZagForward;
    public Transform zigZagBackward;
    private bool forwardZigZag = false;
    private float distanceChase;
    private bool chasePlayer = false;
    private bool pivot;
    private bool positionChecked;
    private bool moveBack;
    private int pivotDirection;


    //Waiting to attack
    public float minWaitBeforeAttack = 1.5f;
    public float maxWaitBeforeAttack = 6.5f;
    public float distanceBeforeImidiateAttack = 1.3f;

    private bool attackInvoked = false;
    private bool animationPlaying = false;

    //Stunning parameters
    private bool isStunned = false;
    private bool canBeStunned = false;

    //Overrides
    protected override void UpdateSense()
    {
        //Debug.Log(Vector3.Distance(player.transform.position, transform.position));
        //Distance control
        distanceChase = distanceAttack + 1.5f;


        //Idle turn
        if (canIdleTurn)
        {
            transform.Rotate(Vector3.up * idleTurn * Time.deltaTime);
        }
        //Moving towards the player
        if (chasePlayer && !animationPlaying)
        {
            agent.SetDestination(player.transform.position);
            if (Vector3.Distance(player.transform.position, transform.position) < distanceAttack)
            {
                InAttackRange();
                StopMovingToDestination();
                CanPivot();
            }
            else if (Vector3.Distance(player.transform.position, transform.position) > distanceChase)
            {
                CanMoveToDestination();
                CannotPivot();
            }
        }
        else if (!chasePlayer && !animationPlaying)
        {
            agent.speed = slowWalkingSpeed;
            agent.SetDestination(homePoint);
        }

        //Stunning the enemy
        if (Input.GetKeyDown(KeyCode.Space) && canBeStunned && Vector3.Angle(player.transform.forward, player.transform.position - player.transform.position) < 60f && Vector3.Distance(player.transform.position, transform.position) < 2f)
        {
            if (!isStunned)
            {
                lethal = false;
                isStunned = true;
                enemyAnim.SetTrigger("Stunned");
                swordRenderer.materials[2] = swordMat;
            }
        }

        //In attack range
        if (inAttackRange && !moveBack)
        {
            if (!animationPlaying)
            {
                agent.speed = slowWalkingSpeed;

                //Pivoting around the player
                if (pivot)
                {
                    if (!positionChecked)
                    {
                        pivotTrans.position = player.transform.position;
                        pivotDirection = Random.Range(0, 2);
                        positionChecked = true;
                        Invoke("ChangePivotDirection", Random.Range(minWaitBeforeAttack, maxWaitBeforeAttack - 2.5f));
                        Invoke("ChangePivotDirection", Random.Range(minWaitBeforeAttack + 2.5f, maxWaitBeforeAttack));
                    }

                    pivotTrans.parent = null;
                    transform.parent = pivotTrans;
                    if (pivotDirection == 0)
                    {
                        pivotTrans.Rotate(Vector3.up * pivotSpeed * Time.deltaTime);
                    }
                    else if (pivotDirection == 1)
                    {
                        pivotTrans.Rotate(-Vector3.up * pivotSpeed * Time.deltaTime);
                    }

                    //ZigZag
                    if (Vector3.Distance(player.transform.position, transform.position) < distanceAttack - 1) { forwardZigZag = false;}
                    else if (Vector3.Distance(player.transform.position, transform.position) > distanceAttack + 1){ forwardZigZag = true; }

                    if (!forwardZigZag)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, zigZagBackward.position, backAndForwardSpeed * Time.deltaTime);
                    }
                    else if (forwardZigZag)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, zigZagForward.position, backAndForwardSpeed * Time.deltaTime);
                    }


                }


                //Attacking the player
                if (!attackInvoked)
                {
                    Invoke("Attack", Random.Range(minWaitBeforeAttack, maxWaitBeforeAttack));
                    attackInvoked = true;
                }
                else if(Vector3.Distance(transform.position, player.transform.position) < distanceBeforeImidiateAttack)
                {
                    CancelInvoke();
                    Attack();
                }
            }
        }

        //Move back after an attack
        if (moveBack)
        {
            var backedUpPos = transform.position - transform.forward;
            transform.position = Vector3.MoveTowards(transform.position ,backedUpPos, stepBackSpeed * Time.deltaTime);
        }
    }
    public override void InAttackRange() { inAttackRange = true;}
    public override void Attack()
    {
        animationPlaying = true;
        enemyAnim.SetInteger("Swing", Random.Range(1, 4));
        CanMoveToDestination();
        CannotPivot();
    }
    public override void EnemyInSight() { chasePlayer = true; }
    public override void EnemyOutOfSight() { chasePlayer = false; idle = true; }
    public override void Idle() 
    {
        if (idle)
        {
            if (!patrol)
            {
                enemyAnim.SetBool("Idle", true);
                if (idleTurnedDone)
                {
                    Invoke("TurnWhileIdle", Random.Range(4, 10));
                    idleTurnedDone = false;
                }
            }
            else if (patrol)
            {
                Debug.Log("Yes");
                if(numberOfMoves < patrolDestinations.Length && !moveToPatrolDestination)
                {
                    agent.SetDestination(patrolDestinations[numberOfMoves].position);
                    Debug.Log("Nope");
                    moveToPatrolDestination = true;
                }
                else if(moveToPatrolDestination && Vector3.Distance(patrolDestinations[numberOfMoves].position, transform.position) < 0.2f)
                {
                    Debug.Log("Arrived");
                    moveToPatrolDestination = false;
                    numberOfMoves++;
                }
                else if(numberOfMoves >= patrolDestinations.Length)
                {
                    numberOfMoves = 0;
                }
            }
        }
        else if (!idle)
        {
            enemyAnim.SetBool("Idle", false);
        }
    }
    


    //Other functions
    public void CanBeStunned()
    {
        agent.SetDestination(player.transform.position);
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
    }
    public void NotLethal()
    {
        lethal = false;
    }
    public void ChangeSwordMat(Material newMat)
    {
        var tempMaterials = swordRenderer.materials;
        tempMaterials[2] = newMat;
        swordRenderer.materials = tempMaterials;
    }
    private void StopMovingToDestination() { agent.isStopped = true; }
    private void CanMoveToDestination() 
    { 
        agent.isStopped = false;
        inAttackRange = false;
        agent.speed = movementSpeed;
    }
    private void CanPivot() { pivot = true; }
    private void CannotPivot()
    {
        pivot = false;
        positionChecked = false;
        transform.parent = null;
        pivotTrans.position = transform.position;
        pivotTrans.parent = transform;
        positionChecked = false;
    }
    private void ChangePivotDirection()
    {
        pivotSpeed = - pivotSpeed;
    }
    private void TurnWhileIdle()
    {
        int chooseDir = Random.Range(0,2);
        if(chooseDir == 0)
        {
            idleTurn = -Random.Range(30, 180);
        }
        else
        {
            idleTurn = Random.Range(30, 180);
        }
        
        canIdleTurn = true;
        Invoke("CannotIdleTurn", 0.7f);
    }
    private void CannotIdleTurn()
    {
        canIdleTurn = false;
        idleTurnedDone = true;
    }
    private void MovingBack() 
    { 
        moveBack = true;
        Invoke("StopMovingBack", 0.2f);
    }
    private void StopMovingBack()
    {
        moveBack = false;
    }
    public void AnimationDone()
    {
        animationPlaying = false;
        isStunned = false;
        attackInvoked = false;
        enemyAnim.SetInteger("Swing", 0);
        MovingBack();
    }
}
