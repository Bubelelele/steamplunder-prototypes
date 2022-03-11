using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AImelee: EnemyClass
{
    [SerializeField] private int attackDamage;
    
    private Animator attackAnim;
    private Rigidbody rb;
    private NavMeshAgent nma;
    [HideInInspector]
    public string attackParameter = "Attack";
    private readonly Collider[] _attackHitboxResults = new Collider[5];
    private void Start()
    {
        attackAnim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
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

            StartCoroutine(AttackHitRegDelay());
        }

        if (playerInSightRange && !playerInAttackRange)
            aiState = EnemyStates.chase;
    }

    IEnumerator AttackHitRegDelay() {
        yield return new WaitForSeconds(.3f);
        //Check hitbox and act accordingly
        var t = transform;
        int numberHit = Physics.OverlapCapsuleNonAlloc(t.position + t.forward + t.right * .5f, t.position + t.forward + t.right * -.5f, .7f, _attackHitboxResults, playerLayer);
        for (int i = 0; i < numberHit; i++) {
            if (_attackHitboxResults[i].transform.CompareTag("Player")) {
                _attackHitboxResults[i].GetComponent<PlayerStats>().Damage(attackDamage);
                EffectManager.instance.BloodSplat(_attackHitboxResults[i].ClosestPointOnBounds(transform.position));
            }
        }
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

    private IEnumerator StunCooldown() {
        yield return new WaitForSeconds(1f);
        stunned = false;
        nma.enabled = true;
        rb.isKinematic = true;
    }


}
