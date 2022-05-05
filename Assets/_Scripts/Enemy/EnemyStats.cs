using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour, IDamageable {

    [SerializeField] private int maxHealth = 50;
    [SerializeField] private Healthbar healthbar;
    [SerializeField] private DamageFlash damageFlash;
    
    private int _health;

    private void Start() {
        _health = maxHealth;
        healthbar.UpdateHealthbar(_health, maxHealth);
    }

    public void Damage(int amount) {
            _health -= amount;
            if (_health <= 0) Die();

            damageFlash?.Flash();
            healthbar.UpdateHealthbar(_health, maxHealth);
    }

    private void Die() {
        var position = transform.position;
        EffectManager.instance.DeathEffect(position);
        Destroy(gameObject);
        EffectManager.instance.CogPickup(position);
    }
}