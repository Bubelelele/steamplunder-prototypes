using System.Collections;
using UnityEngine;

public class ProjectileFromBoss : MonoBehaviour {

    public int damageAmount;

    private ParticleSystem _particles;
    
    private void Start() {
        _particles = GetComponent<ParticleSystem>();
        _particles.Play();

        StartCoroutine(DestroySelf());
    }

    private void FixedUpdate() {
        transform.position += transform.forward * 0.3f;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerStats>().Damage(5);
        }
        
        _particles.Play();
        Destroy(gameObject);
    }

    private IEnumerator DestroySelf() {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}