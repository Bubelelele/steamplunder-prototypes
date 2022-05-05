using UnityEngine;
using UnityEngine.AI;
public abstract class EnemyBase : MonoBehaviour 
{
    [Header("Common enemy paramaters")]
    public float movementSpeed;
    public float slowWalkingSpeed;
    public float rotationSpeed;
    public float sightRange;
    public float FOV;
    public int attackDamage;
    

    [HideInInspector] public GameObject player;
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public Animator enemyAnim;
    [HideInInspector] public Vector3 homePoint;
    [HideInInspector] public bool idle;
    [HideInInspector] public bool inAttackRange;
    [HideInInspector] public bool chasePlayer;
    [HideInInspector] public bool animationPlaying;

    private float alertRange = 20;
    private float rangeForStopChasingPlayer = 20;
    private bool playerDetected = false;
    private bool checkedForNerbyEnemies = false;
    private bool calledByNerbyEnemies = false;
    private GameObject alertTrigger;


    [Header("Idle patroling")]
    //Idle
    public bool patrol;
    public float patrolRange = 10f;
    //public Transform[] patrolDestinations;

    private Vector3 randomPos;
    private float timeSinceLastMoved;
    
    //private int numberOfMoves;
    private bool moveToPatrolDestination = true;
    private float idleTurn;
    private bool randomCheck = false;
    private bool canIdleTurn = false;
    private bool idleTurnedDone = true;
    private bool changeDestinationNumber = true;



    protected virtual void Initialize() { }
    protected virtual void UpdateSense() { }

    private void Start()
    {
        player = GameManager.instance.player.gameObject;
        agent = gameObject.GetComponent<NavMeshAgent>();
        enemyAnim = gameObject.GetComponent<Animator>();
        homePoint = transform.position;
        alertTrigger = transform.GetComponentInChildren<AlertTrigger>().gameObject;
        Initialize();
    }

    private void Update()
    {
        UpdateSense();
        Idle();

        //For the idle movement
        timeSinceLastMoved += Time.deltaTime;

        //Idle turn
        if (canIdleTurn)
        {
            transform.Rotate(Vector3.up * idleTurn * Time.deltaTime);
        }

        if (!calledByNerbyEnemies)
        {
            if (Vector3.Distance(player.transform.position, transform.position) < sightRange && Vector3.Angle(transform.forward, player.transform.position - transform.position) < FOV / 2)
            {
                EnemyInSight();
                playerDetected = true;
            }
            else if(Vector3.Distance(player.transform.position, transform.position) < 3f)
            {
                EnemyInSight();
                playerDetected = true;
            }
            else
            {
                EnemyOutOfSight();
                idle = true;
                playerDetected = false;
                checkedForNerbyEnemies = false;
            }
        }
        else
        {
            if (Vector3.Distance(player.transform.position, transform.position) > rangeForStopChasingPlayer)
            {
                calledByNerbyEnemies = false;
                playerDetected = false;
                alertTrigger.transform.localScale = Vector3.one;
                idle = true;
                EnemyOutOfSight();
                
            }
        }
        

        if (playerDetected)
        {
            PlayerDetectedCommon();
            if (Vector3.Distance(player.transform.position, transform.position) < sightRange && Vector3.Angle(transform.forward, player.transform.position - transform.position) < FOV / 2)
            {
                EnemyInSight();
            }
        }

    }
    public void AwareOfPlayer()
    {
        playerDetected = true;
        calledByNerbyEnemies = true;
    }
    public void PlayerDetectedCommon()
    {
        idle = false;
        var targetRotation = Quaternion.LookRotation(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z) - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        if (!checkedForNerbyEnemies)
        {
            alertTrigger.transform.localScale = Vector3.Slerp(alertTrigger.transform.localScale, Vector3.one * alertRange, 0.6f * Time.deltaTime);
            if (alertTrigger.transform.localScale.magnitude >= alertRange)
            {
                checkedForNerbyEnemies = true;
            }
        }
    }
    public void Idle()
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
                    if (Vector3.Distance(transform.position, randomPos) < 1 || timeSinceLastMoved > 4) { moveToPatrolDestination = false; }
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
    public void EnemyInSight() { chasePlayer = true; }
    public void EnemyOutOfSight() { chasePlayer = false; idle = true; }
    public abstract void Attack();
    public void InAttackRange() { inAttackRange = true; }
    public void StopMovingToDestination() { agent.isStopped = true; }
    public void CanMoveToDestination(float speed)
    {
        agent.isStopped = false;
        inAttackRange = false;
        agent.speed = speed;
    }

    private void NextDestination()
    {
        moveToPatrolDestination = true;
        randomCheck = false;
        changeDestinationNumber = true;
    }
    private void TurnWhileIdle()
    {
        int chooseDir = Random.Range(0, 2);
        if (chooseDir == 0)
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
}
