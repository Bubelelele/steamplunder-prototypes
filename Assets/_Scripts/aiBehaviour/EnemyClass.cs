using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class EnemyClass : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask groundLayer, playerLayer;
    public float timeBetweenAttacks;
    public float sightRange, attackRange;

    [HideInInspector]
    public Vector3 homePoint, sightOrigin;
    [HideInInspector]
    public bool alreadyAttacked, playerInSightRange, playerInAttackRange;
    [HideInInspector]
    public EnemyStates aiState = EnemyStates.idle;

    protected bool stunned;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        homePoint = transform.position;
    }

    public virtual void Update() {
        if (stunned) return;
        //check for sigh and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        if (aiState != EnemyStates.idle)
            PlayerInRange();

        switch (aiState)
        {
            case EnemyStates.idle:
                Idle();
                break;
            case EnemyStates.chase:
                ChasePlayer();
                break;
            case EnemyStates.attack:
                AttackPlayer();
                break;
        }

    }

    public void PlayerInRange()
    {
        if (!playerInSightRange && !playerInAttackRange)
            aiState = EnemyStates.idle;
    }

    public abstract void Idle();

    public abstract void ChasePlayer();

    public abstract void AttackPlayer();

    public abstract void ResetAttack();
}
