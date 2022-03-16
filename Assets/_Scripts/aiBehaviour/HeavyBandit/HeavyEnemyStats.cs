using UnityEngine;
using UnityEngine.UI;

public class HeavyEnemyStats : MonoBehaviour, IDamageable
{
    [HideInInspector] public bool leathal = false;
    [HideInInspector] public bool raisedUp = true;


    [SerializeField] private int maxHealth = 100;
    [SerializeField] private Healthbar healthbar;
    [SerializeField] private DamageFlash damageFlash;
    [SerializeField] private ParticleSystem spark;
    [SerializeField] private GameObject heavyBanditCart;

    private bool canBeHarmed = true;

    private int _health;

    private void Start()
    {
        _health = maxHealth;
        healthbar.UpdateHealthbar(_health, maxHealth);
    }

    public void Damage(int amount)
    {
        if (canBeHarmed)
        {
            _health -= amount;
            if (_health <= 0) Die();

            damageFlash?.Flash();
            healthbar.UpdateHealthbar(_health, maxHealth);
        }
        else
        {
            spark.Play();
            AudioManager.instance.Play("hitshield");
        }

    }

    private void Die()
    {
        var position = transform.position;
        EffectManager.instance.DeathEffect(position);
        Destroy(heavyBanditCart);
        EffectManager.instance.CogPickup(position);
        EffectManager.instance.CogPickup(position);
        
    }
    public void CanBeHarmed(){ 
        canBeHarmed = true;
        gameObject.tag = "NotBlocking";
    }
    public void CannotBeHarmed(){  
        canBeHarmed = false;
        gameObject.tag = "IsBlocking";
    }
    public void NotLeathal() { leathal = false; }
    public void IsLeathal()
    {
        leathal = true;
        AudioManager.instance.Play("swing");
    }
    public void NotRaisedUp(){  raisedUp = false;}
    public void RaisedUp(){  raisedUp = true;}
}
