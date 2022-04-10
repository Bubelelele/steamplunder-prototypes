using UnityEngine;
using UnityEngine.UI;

public class SC_Stats : MonoBehaviour, IDamageable
{

    [SerializeField] private int maxHealth = 50;
    [SerializeField] private Healthbar healthbar;
    [SerializeField] private DmgFlash_SteampunkColossus damageFlash;
    [SerializeField] private GameObject feet;
    [SerializeField] private Animator bossAnim;
    
    private bool canBeHarmed = false;
    private bool secondPhaseDone = false;
    private int numberOfDoors = 0;

    [HideInInspector] public int _health;
    [HideInInspector] public bool isActive;

    private void Start()
    {
        isActive = false;
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

    }

    private void Die()
    {
        var position = transform.position;
        EffectManager.instance.DeathEffect(position);
        Destroy(gameObject);
        Destroy(feet);
        EffectManager.instance.CogPickup(position);
    }
    public void ActivateBoss()
    {
        isActive = true;
        canBeHarmed = true;
    }
    public void DeactivateBoss()
    {
        isActive = false;
        canBeHarmed = false;
    }

    public void DoorOff()
    {
        numberOfDoors++;
        if (numberOfDoors >= 3 && !secondPhaseDone)
        {
            bossAnim.SetBool("SecondPhase", true);
            secondPhaseDone = true;
        } 
    }

}