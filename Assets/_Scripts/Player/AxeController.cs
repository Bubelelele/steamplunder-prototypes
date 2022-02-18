using System;
using System.Collections;
using UnityEngine;

public class AxeController : MonoBehaviour {

    [Header("Settings")]
    [SerializeField] private int attackDamage = 20;
    [SerializeField] private float attackCooldown = .5f;
    [SerializeField] private LayerMask interactableLayer;
    
    [Header("References")]
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private GameObject axeObject;

    private Animator _animator;
    private readonly Collider[] _attackHitboxResults = new Collider[5];
    
    public bool CanAttack { get; private set; } = true;
    public event Action onAttackFinished;
    
    private void Start() {
        _animator = GetComponent<Animator>();
    }

    public void Slash() {
        //Attack visuals
        _animator.SetTrigger("axe");
        trailRenderer.enabled = true;
        
        //Attack cooldown
        CanAttack = false;
        StartCoroutine(nameof(AttackCooldown));

        //Check hitbox and act accordingly
        var t = transform;
        int numberHit = Physics.OverlapCapsuleNonAlloc(t.position + t.forward + t.right * .5f, t.position + t.forward + t.right * -.5f, .7f, _attackHitboxResults, interactableLayer);
        for (int i = 0; i < numberHit; i++) {
            var damageable = _attackHitboxResults[i].transform.GetComponent<IDamageable>();
            if (damageable != null) {
                damageable.Damage(attackDamage);
                EffectManager.instance.BloodSplat(_attackHitboxResults[i].ClosestPointOnBounds(axeObject.transform.position));
            }
        }
    }

    private IEnumerator AttackCooldown() {
        UIManager.instance.cooldownsBar?.AxeCooldown(attackCooldown);
        yield return new WaitForSeconds(attackCooldown);
        CanAttack = true;
    }

    private void AxeActionFinished() {
        trailRenderer.enabled = false;
        onAttackFinished?.Invoke();
    }

    public void SetObjectActive(bool state) {
        axeObject.SetActive(state);
    }
    
/*
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        var t = transform;
        Gizmos.DrawSphere(t.position + t.forward + t.right * .5f, .7f);
        Gizmos.DrawSphere(t.position + t.forward + t.right * -.5f, .7f);
    }*/
}