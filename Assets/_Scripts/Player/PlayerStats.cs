using System.Collections;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamageable {
    
    [SerializeField] private int maxHealth = 50;
    [SerializeField] private Healthbar healthbar;
    
    private int _health;

    private void Start() {
        _health = maxHealth;
    }

    public void Damage(int amount) {
        _health -= amount;
        if (_health <= 0) Die();
        
        healthbar.UpdateHealthbar(_health, maxHealth);
    }

    private void Die() {
        GameManager.instance.UpdateGameState(GameState.NoMove);
        EffectManager.instance.DeathEffect(transform.position);
        GameManager.instance.WaitReloadScene(1f);
        gameObject.SetActive(false);
    }
    
}