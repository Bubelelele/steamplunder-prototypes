using System;
using System.Collections;
using UnityEngine;

public class GunController : MonoBehaviour {
    
    [Header("Settings")]
    [SerializeField] private int attackDamage = 50;
    [SerializeField] private float attackCooldown = 5f;
    [SerializeField] private float recoilForce = 5f;
    
    [Header("References")]
    [SerializeField] private Transform muzzleTrans;
    [SerializeField] private GameObject projectilePrefab;

    private Animator _animator;
    private Rigidbody _rb;
    
    public bool CanAttack { get; private set; } = true;
    public event Action onAttackFinished;
    
    private void Start() {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
    }

    public void Aim() {

        AudioManager.instance.Play("gun");

        //Attack visuals
        _animator.SetTrigger("shoot");
        
        //Attack cooldown
        CanAttack = false;
        StartCoroutine(nameof(AttackCooldown));
    }

    private void Shoot() {

        //Spawn projectile
        Quaternion projectileRotation = Quaternion.Euler(0f, muzzleTrans.rotation.eulerAngles.y, 0f);
        Projectile projectile = Instantiate(projectilePrefab, muzzleTrans.position, projectileRotation).GetComponent<Projectile>();
        projectile.damageAmount = attackDamage;
        
        //Add recoil to the player
        Vector3 recoilForceVector = -transform.forward * recoilForce;
        _rb.AddForce(recoilForceVector, ForceMode.Impulse);

        GunActionFinished();
    }

    private IEnumerator AttackCooldown() {
        UIManager.instance.cooldownsBar?.GunCooldown(attackCooldown);
        yield return new WaitForSeconds(attackCooldown);
        CanAttack = true;
    }

    private void GunActionFinished() {
        onAttackFinished?.Invoke();
    }

}