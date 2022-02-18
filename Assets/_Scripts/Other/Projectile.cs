using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public int damageAmount;
    
    private ParticleSystem _particles;
    
    private void Start() {
        _particles = GetComponent<ParticleSystem>();
        _particles.Play();

        StartCoroutine(DestroySelf());
    }

    private void FixedUpdate() {
        transform.position += transform.forward * .3f;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Enemy")) {
            EnemyStats enemy = other.GetComponent<EnemyStats>();
            if (enemy != null) enemy.Damage(damageAmount);
        }
        
        _particles.Play();
        Destroy(gameObject);
    }

    private IEnumerator DestroySelf() {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}