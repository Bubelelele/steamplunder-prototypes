using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : EnemyBase
{
    //Paramaters to other scripts
    [HideInInspector] public bool lethal = false;
    [HideInInspector] public Vector3 targetPos;

    [Header("Melee paramaters")]
    //Idle
    public bool patrol;
    public float patrolRange = 10f;
    //public Transform[] patrolDestinations;

    private Vector3 randomPos;
    public float idleTurn = 40f;
    private float timeSinceLastMoved;
    //private int numberOfMoves;
    private bool moveToPatrolDestination = true;
    private bool randomCheck = false;
    private bool canIdleTurn = false;
    private bool idleTurnedDone = true;
    private bool changeDestinationNumber = true;



    //Movement
    public float slowWalkingSpeed = 5f;
    public float pivotSpeed = 20f;
    public float zigZagSpeed = 2f;

    [Header("References")]
    public Transform pivotTrans;
    public Transform zigZagForward;
    public Transform zigZagBackward;


    private float distanceAttack = 3f;
    private float distanceChase;
    private float stepBackSpeed = 5f;
    private int pivotDirection;
    private int forwardOrBackwards;
    private bool forwardZigZag = false;
    private bool chasePlayer = false;
    private bool pivot;
    private bool positionChecked;
    private bool moveBack;

    //Sword
    public Renderer swordRenderer;
    public Material swordMat, swordMatLight;


    //Waiting to attack
    private float minWaitBeforeAttack = 1.5f;
    private float maxWaitBeforeAttack = 6.5f;
    private float distanceBeforeImidiateAttack = 1.3f;

    private bool inAttackRange;
    private bool attackInvoked = false;
    private bool animationPlaying = false;

    //Stunning parameters
    private bool isStunned = false;
    private bool canBeStunned = false;
    

    //Overrides
    protected override void UpdateSense()
    {
        //Distance control
        distanceChase = distanceAttack + 1.5f;

        //For the idle movement
        timeSinceLastMoved += Time.deltaTime;


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
            else if (Vector3.Distance(player.transform.position, transform.position) > distanceChase && !idle)
            {
                CanMoveToDestination(movementSpeed);
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
                Stun();
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
                        forwardOrBackwards = Random.Range(0, 2);
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
                        transform.position = Vector3.MoveTowards(transform.position, zigZagBackward.position, zigZagSpeed * Time.deltaTime);
                    }
                    else if (forwardZigZag)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, zigZagForward.position, zigZagSpeed * Time.deltaTime);
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
        CanMoveToDestination(movementSpeed);
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
                
                if (moveToPatrolDestination)
                {
                    if (!randomCheck)
                    {
                        timeSinceLastMoved = 0;
                        Vector3 randomDestination = Random.insideUnitSphere * patrolRange;
                        randomDestination += homePoint;
                        NavMeshHit hit;
                        NavMesh.SamplePosition(randomDestination, out hit, patrolRange, 1);
                        randomPos = hit.position;
                        randomCheck = true;
                    }
                    CanMoveToDestination(slowWalkingSpeed);
                    agent.SetDestination(randomPos);
                    if (Vector3.Distance(transform.position, randomPos) < 1 || timeSinceLastMoved > 4){  moveToPatrolDestination = false;}
                }
                else if (!moveToPatrolDestination)
                {
                    
                    StopMovingToDestination();
                    enemyAnim.SetBool("Idle", true);
                    if (changeDestinationNumber)
                    {
                        Invoke("NextDestination", Random.Range(5, 15));
                        //if (numberOfMoves < patrolDestinations.Length -1)
                        //{
                        //    numberOfMoves++;
                        //}
                        //else
                        //{
                        //    numberOfMoves = 0;
                        //}
                        changeDestinationNumber = false;
                    }
                }
            }
        }
        else if (!idle)
        {
            enemyAnim.SetBool("Idle", false);
        }
    }



    //Other functions
    public void Stun()
    {
        enemyAnim.SetTrigger("Stunned");
        isStunned = true;
        swordRenderer.materials[2] = swordMat;
        lethal = false;
    }
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
    private void NextDestination() 
    { 
        moveToPatrolDestination = true;
        randomCheck = false;
        changeDestinationNumber = true;
    }
    private void StopMovingToDestination() { agent.isStopped = true; }
    private void CanMoveToDestination(float speed) 
    { 
        agent.isStopped = false;
        inAttackRange = false;
        agent.speed = speed;
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
