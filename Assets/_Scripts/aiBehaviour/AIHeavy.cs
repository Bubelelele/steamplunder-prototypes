using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIHeavy: EnemyClass
{
    [SerializeField] private int attackDamage;
    [SerializeField] GameObject heavyBandit;
    
    private Animator attackAnim;
    private Rigidbody rb;
    private NavMeshAgent nma;
    [HideInInspector]
    public string attackParameter = "Attack";
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

        if (playerInSightRange && !playerInAttackRange)
            aiState = EnemyStates.chase;
    }

    public override void ChasePlayer()
    {
        agent.SetDestination(player.position);
        attackAnim.SetBool("Block", true);
        if (playerInSightRange && playerInAttackRange)
            aiState = EnemyStates.attack;
    }

    public override void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        if (!alreadyAttacked)
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
        transform.LookAt(player);
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
    }

    private IEnumerator StunCooldown() {
        yield return new WaitForSeconds(1f);
        stunned = false;
        nma.enabled = true;
        rb.isKinematic = true;
    }
}
