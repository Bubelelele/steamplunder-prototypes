using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AIHeavy: EnemyClass
{
    public int attackDamage;
    [HideInInspector] public string attackParameter = "Attack";

    [SerializeField] GameObject heavyBandit;
    
    private Animator attackAnim;
    private Rigidbody rb;
    private NavMeshAgent nma;


    private readonly Collider[] _attackHitboxResults = new Collider[5];
    private void Start()
    {
        attackAnim = heavyBandit.GetComponent<Animator>();
        rb = heavyBandit.GetComponent<Rigidbody>();
        nma = GetComponent<NavMeshAgent>();
        player = GameManager.instance.player;
    }

    public override void Idle()
    {
        agent.SetDestination(homePoint);
        gameObject.GetComponent<RotateToPlayer>().DontLookAtPlayer();

        if (playerInSightRange && !playerInAttackRange)
            aiState = EnemyStates.chase;
    }

    public override void ChasePlayer()
    {
        if (heavyBandit.GetComponent<HeavyEnemyStats>().raisedUp)
        {
            gameObject.GetComponent<RotateToPlayer>().LookAtPlayer();
            agent.SetDestination(player.position);
            attackAnim.SetBool("Block", true);
            if (playerInSightRange && playerInAttackRange)
                aiState = EnemyStates.attack;
        }

    }

    public override void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        if (attackAnim != null && !alreadyAttacked)
        {
            attackAnim.SetTrigger(attackParameter);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }

        if (playerInSightRange && !playerInAttackRange)
            aiState = EnemyStates.chase;
    }


    public override void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void Stun() {
        stunned = true;
        nma.enabled = false;
        rb.isKinematic = false;
        StartCoroutine(StunCooldown());
    }
    public void Stunned()
    {
        attackAnim.SetTrigger("Stunned");
        gameObject.GetComponent<RotateToPlayer>().DontLookAtPlayer();
    }

    private IEnumerator StunCooldown() {
        yield return new WaitForSeconds(1f);
        stunned = false;
        nma.enabled = true;
        rb.isKinematic = true;
    }
}
