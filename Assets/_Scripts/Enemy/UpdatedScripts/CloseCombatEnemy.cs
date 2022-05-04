using UnityEngine;

public class CloseCombatEnemy : EnemyBase
{
    //Paramaters to other scripts
    [HideInInspector] public bool lethal = false;
    [HideInInspector] public Vector3 targetPos;

    //Other
    private bool inAttackRange;

    //Sword
    public Renderer swordRenderer;
    public Material swordMat, swordMatLight;

    //Movement
    public Transform pivotTrans;
    public float distanceAttack = 3f;
    public float distanceChase = 5f;
    public float pivotSpeed = 20f;
    public float stepBackSpeed = 5f;
    public float slowWalkingSpeed = 5f;

    private bool chasePlayer = false;
    private bool pivot;
    private bool positionChecked;
    private bool moveBack;
    private int pivotDirection;


    //Waiting to attack
    public float minWaitBeforeAttack = 1.5f;
    public float maxWaitBeforeAttack = 6.5f;
    public float distanceBeforeImidiateAttack = 2f;

    private bool attackInvoked = false;
    private bool animationPlaying = false;

    //Stunning parameters
    private bool isStunned = false;
    private bool canBeStunned = false;

    //Overrides
    protected override void UpdateSense()
    {

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

        //Stunning the enemy
        if (Input.GetKeyDown(KeyCode.Space) && canBeStunned)
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
                }

                //Attacking the player
                if (!attackInvoked)
                {
                    Invoke("Attack", Random.Range(minWaitBeforeAttack, maxWaitBeforeAttack));
                    attackInvoked = true;
                }

                if(Vector3.Distance(transform.position, player.transform.position) < distanceBeforeImidiateAttack)
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
    public override void EnemyOutOfSight() { chasePlayer = false; }
    public override void Idle() 
    {

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
        moveBack = true;
        Invoke("StopMovingBack", 0.2f);
    }
}
