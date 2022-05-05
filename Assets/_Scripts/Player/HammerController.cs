using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class HammerController : MonoBehaviour {
    
    [Header("Settings")]
    [SerializeField] private int attackDamage = 30;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private float impactRadius = 1f;
    [SerializeField] private float knockbackAmount = 10f;
    [SerializeField] private LayerMask interactableLayer;
    
    [Header("References")]
    [SerializeField] private GameObject hammerObject;
    [SerializeField] private GameObject boss;


    private Animator _animator;
    private readonly Collider[] _attackHitboxResults = new Collider[5];
    
    public bool CanAttack { get; private set; } = true;
    public event Action onAttackFinished;
    
    private void Start() {
        _animator = GetComponent<Animator>();
    }
    
    public void Smash() {
        //Attack visuals
        _animator.SetTrigger("hammer");
        SetObjectActive(true);
        //Attack cooldown
        CanAttack = false;
        StartCoroutine(nameof(AttackCooldown));

        Invoke("PlayHammerSound", 0.4f);
    }

    public void PlayHammerSound()
    {
        AudioManager.instance?.Play("hammer");
    }

    //Triggers when the hammer impacts the ground
    private void Impact() {
        var t = transform;
        Vector3 impactPosition = t.position + t.forward * 1.5f + t.right * .5f + t.up * -1f;
        EffectManager.instance.AOE(impactPosition, impactRadius); //Spawn hit effect

        int numberHit = Physics.OverlapSphereNonAlloc(impactPosition, impactRadius, _attackHitboxResults, interactableLayer);
        for (int i = 0; i < numberHit; i++) {
            var damageable = _attackHitboxResults[i].transform.GetComponent<IDamageable>();
            if (_attackHitboxResults[i].gameObject.tag == "Boss")
            {
                boss.GetComponent<AttackScript>().Stunned();
            }
            else
            {
                damageable?.Damage(attackDamage);
            }
            //Add knockback if entity has a rigidbody
            var rb = _attackHitboxResults[i].transform.GetComponent<Rigidbody>();
            if (rb != null) {
                MeleeEnemy meleeEnemy = rb.GetComponent<MeleeEnemy>();
                if (meleeEnemy != null) 
                {
                    meleeEnemy.Stun();
                }
                HeavyEnemy heavyEnemy = rb.GetComponent<HeavyEnemy>();
                if (heavyEnemy != null)
                {
                    heavyEnemy.Stun();
                }

                Vector3 colliderHitPosition = _attackHitboxResults[i].ClosestPointOnBounds(impactPosition);
                float distance = Vector3.Distance(impactPosition, colliderHitPosition);
                float multiplier = 1f - Mathf.Clamp01(distance / impactRadius);
                
                Vector3 knockbackDirection = (colliderHitPosition - impactPosition).normalized;
                if (knockbackDirection.sqrMagnitude == 0) knockbackDirection = transform.forward; //If impact is inside the bounds of the collider, it is a zero-vector
                Vector3 knockbackVector = knockbackDirection.normalized * knockbackAmount * multiplier;
                knockbackVector.y *= .1f; //Stop these buggers from flying >:(
                
                rb.AddForce(knockbackVector, ForceMode.Impulse);
            }
        }
        
        HammerActionEnded();
    }
    
    private IEnumerator AttackCooldown() {
        UIManager.instance.cooldownsBar?.HammerCooldown(attackCooldown);
        yield return new WaitForSeconds(attackCooldown);
        CanAttack = true;
    }

    private void HammerActionEnded() {
        SetObjectActive(false);
        onAttackFinished?.Invoke();
    }
    
    private void SetObjectActive(bool state) {
        hammerObject.SetActive(state);
    }
    
    /*private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        var t = transform;
        Gizmos.DrawSphere(t.position + t.forward * 1.5f + t.right * .5f + t.up * -1f, impactRadius);
    }*/
}