using System;
using System.Collections;
using UnityEngine;

public class SteamerController : MonoBehaviour {
    
    [Header("Settings")]
    [SerializeField] private int attackDamage = 10;
    [SerializeField] private float attackCooldown = 5f;
    [SerializeField] private LayerMask interactableLayer;
    
    [Header("References")]
    [SerializeField] private GameObject steamerObject;
    
    private Animator _animator;
    private readonly Collider[] _attackHitboxResults = new Collider[5];
    
    public bool CanAttack { get; private set; } = true;
    public event Action onAttackFinished;
    
    private void Start() {
        _animator = GetComponent<Animator>();
    }

    public void SuckMoisture() {
        //Attack visuals
        _animator.SetTrigger("steamer");
        SetObjectActive(true);
        
        //Attack cooldown
        CanAttack = false;
        StartCoroutine(nameof(AttackCooldown));
    }
    
    //Called in the animation
    private void Blow() {
        //Check hitbox and act accordingly
        AudioManager.instance.Play("steamer");

        var t = transform;
        int numberHit = Physics.OverlapCapsuleNonAlloc(t.position + t.forward * 1.7f + t.right * .4f, t.position + t.forward * 3.4f + t.right * .4f, .75f, _attackHitboxResults, interactableLayer);
        for (int i = 0; i < numberHit; i++) {
            var damageable = _attackHitboxResults[i].transform.GetComponent<IDamageable>();
            if (damageable != null) damageable.Damage(attackDamage);
        }
    }

    private IEnumerator AttackCooldown() {
        UIManager.instance.cooldownsBar?.SteamerCooldown(attackCooldown);
        yield return new WaitForSeconds(attackCooldown);
        CanAttack = true;
    }

    //Called at the end of the animation
    private void SteamerActionEnded() {
        SetObjectActive(false);
        onAttackFinished?.Invoke();
    }
    
    private void SetObjectActive(bool state) {
        steamerObject.SetActive(state);
    }
    
    /*private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        var t = transform;
        Gizmos.DrawSphere(t.position + t.forward * 1.7f + t.right * .4f, .75f);
        Gizmos.DrawSphere(t.position + t.forward * 3.4f + t.right * .4f, .75f);
    }*/
    
}