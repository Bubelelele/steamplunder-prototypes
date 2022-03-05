using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStats : MonoBehaviour, IDamageable
{

    [SerializeField] private int maxHealth = 50;
    [SerializeField] private Healthbar healthbar;
    [SerializeField] private DamageFlash damageFlash;
    [SerializeField] private GameObject healtbarImage;

    private int _health;

    private void Start()
    {
        _health = maxHealth;
        healthbar.UpdateHealthbar(_health, maxHealth);
    }

    public void Damage(int amount)
    {
        _health -= amount;
        if (_health <= 0) Die();

        damageFlash?.Flash();
        healthbar.UpdateHealthbar(_health, maxHealth);
    }

    private void Die()
    {
        var position = transform.position;
        EffectManager.instance.DeathEffect(position);
        Destroy(gameObject);
        Destroy(healtbarImage);
        EffectManager.instance.CogPickup(position);
    }
}
