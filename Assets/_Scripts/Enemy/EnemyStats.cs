using UnityEngine;

public class EnemyStats : MonoBehaviour, IDamageable {

    [HideInInspector]public bool canBeHarmed = true;


    [SerializeField] private int maxHealth = 50;
    [SerializeField] private Healthbar healthbar;
    [SerializeField] private DamageFlash damageFlash;


    
    private int _health;

    private void Start() {
        _health = maxHealth;
        healthbar.UpdateHealthbar(_health, maxHealth);
    }

    public void Damage(int amount) {
        if (canBeHarmed)
        {
            _health -= amount;
            if (_health <= 0) Die();

            damageFlash?.Flash();
            healthbar.UpdateHealthbar(_health, maxHealth);
        }
    }

    private void Die() {
        var position = transform.position;
        EffectManager.instance.DeathEffect(position);
        Destroy(gameObject);
        EffectManager.instance.CogPickup(position);
    }
    public void CanBeHarmed()
    {
        canBeHarmed = true;
    }
    public void CannotBeHarmed()
    {
        canBeHarmed = false;
    }
}