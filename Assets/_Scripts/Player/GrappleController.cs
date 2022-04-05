using System;
using System.Collections;
using UnityEngine;

public class GrappleController : MonoBehaviour {
    
    [Header("Settings")]
    [SerializeField] private int attackDamage = 5;
    [SerializeField] private float attackCooldown = 5f;
    [SerializeField] private float extendSpeed = 10f;
    [SerializeField] private float retractSpeed = 5f;
    [SerializeField] private float cableRange = 8f;
    [SerializeField] private LayerMask hittableLayers;
    
    [Header("References")]
    [SerializeField] private GameObject grappleObject;
    [SerializeField] private Transform cable;

    private Rigidbody _rb;
    private bool _isExtending;
    private bool _isRetracting;
    private float _lerpTo;
    private bool _hitSomething;
    
    public bool CanAttack { get; private set; } = true;
    public event Action onAttackFinished;

    private void Start() => _rb = GetComponent<Rigidbody>();

    private void Update() {
        if (_isExtending) {
            float newZ = Mathf.MoveTowards(cable.localScale.z, _lerpTo, extendSpeed * Time.deltaTime);
            cable.localScale = new Vector3(1, 1, newZ);

            if (cable.localScale.z == _lerpTo) ExtensionFinished();
        } else if (_isRetracting) {
            if (_hitSomething) transform.position += transform.forward * retractSpeed * 2f * Time.deltaTime;
            
            float newZ = Mathf.MoveTowards(cable.localScale.z, _lerpTo, retractSpeed * Time.deltaTime);
            cable.localScale = new Vector3(1, 1, newZ);

            if (cable.localScale.z == _lerpTo) RetractionFinished();
        }
    }

    public void Extend() {
        GameManager.instance.UpdateGameState(GameState.NoMove);
        SetObjectActive(true);

        AudioManager.instance?.Play("grapplehook");
        
        //Attack cooldown
        CanAttack = false;
        StartCoroutine(nameof(AttackCooldown));
        
        CastCable();
    }

    private void CastCable() {
        Vector3 shootPos = cable.position;
        Ray ray = new Ray(shootPos, cable.forward);

        if (Physics.Raycast(ray, out var hit, cableRange, hittableLayers)) {
            _lerpTo = Vector3.Distance(shootPos, hit.point) * .5f;
            if (hit.transform.GetComponent<GrappleLever>()) {
                hit.transform.GetComponent<GrappleLever>().Pull();
                _hitSomething = false;
                _isExtending = true;
                return;
            }
            _hitSomething = true;
            //Damage potential enemy
            if (hit.transform.CompareTag("Enemy")) hit.transform.GetComponent<IDamageable>().Damage(attackDamage);
            if (hit.transform.GetComponent<AImelee>()) hit.transform.GetComponent<AImelee>().Stun();//Do this differently
        } else {
            _lerpTo = cableRange * .5f;
            _hitSomething = false;
        }

        _isExtending = true;
    }

    private void ExtensionFinished() {
        _isExtending = false;
        _lerpTo = 0f;
        _isRetracting = true;
        
        _rb.useGravity = false;
        _rb.isKinematic = true;
    }

    private void RetractionFinished() {
        _rb.useGravity = true;
        _rb.isKinematic = false;
        
        _isRetracting = false;
        GrappleActionEnded();
    }
    
    private IEnumerator AttackCooldown() {
        UIManager.instance.cooldownsBar?.GrappleCooldown(attackCooldown);
        yield return new WaitForSeconds(attackCooldown);
        CanAttack = true;
    }

    private void GrappleActionEnded() {
        SetObjectActive(false);
        cable.localScale = new Vector3(1, 1, 0);
        GameManager.instance.UpdateGameState(GameState.Default);
        onAttackFinished?.Invoke();
    }
    
    private void SetObjectActive(bool state) {
        grappleObject.SetActive(state);
    }
    
}