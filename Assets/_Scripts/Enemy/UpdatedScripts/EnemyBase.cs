using UnityEngine;
using UnityEngine.AI;
public abstract class EnemyBase : MonoBehaviour 
{
    public float movementSpeed, rotationSpeed, sightRange, FOV, alertRange, rangeForStopChasingPlayer;
    public int attackDamage;
    public GameObject alertTrigger;

    [HideInInspector] public GameObject player;
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public Animator enemyAnim;
    [HideInInspector] public Vector3 homePoint;

    private bool playerDetected = false;
    private bool checkedForNerbyEnemies = false;
    private bool calledByNerbyEnemies = false;

    protected virtual void Initialize() { }
    protected virtual void UpdateSense() { }

    private void Start()
    {
        player = GameManager.instance.player.gameObject;
        agent = gameObject.GetComponent<NavMeshAgent>();
        enemyAnim = gameObject.GetComponent<Animator>();
        homePoint = transform.position;
        Initialize();
    }

    private void Update()
    {
        UpdateSense();
        if (!calledByNerbyEnemies)
        {
            if (Vector3.Distance(player.transform.position, transform.position) < sightRange && Vector3.Angle(transform.forward, player.transform.position - transform.position) < FOV / 2)
            {
                EnemyInSight();
                playerDetected = true;
            }
            else
            {
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
                EnemyOutOfSight();
            }
        }
        

        if (playerDetected)
        {
            PlayerDetectedCommon();
            if (Vector3.Angle(transform.forward, player.transform.position - transform.position) < FOV / 2)
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
        var targetRotation = Quaternion.LookRotation(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z) - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        if (!checkedForNerbyEnemies)
        {
            alertTrigger.transform.localScale = Vector3.Slerp(alertTrigger.transform.localScale, Vector3.one * alertRange, 0.4f * Time.deltaTime);
            if (alertTrigger.transform.localScale.magnitude >= alertRange)
            {
                alertTrigger.transform.localScale = Vector3.one;
                checkedForNerbyEnemies = true;
            }
        }
    }
    public abstract void EnemyInSight();
    public abstract void EnemyOutOfSight();
    public abstract void InAttackRange();
    public abstract void Attack();
}
