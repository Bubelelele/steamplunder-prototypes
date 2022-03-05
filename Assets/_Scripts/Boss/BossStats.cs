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
    [HideInInspector] public bool canBeHarmed = true;

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
            if (_health <= 0)
            {
                Die();
            }
            else if (_health <= maxHealth / 3 * 2 && !gameObject.GetComponent<BossMovement>().secondStage)
            {
                gameObject.GetComponent<BossMovement>().Stage2();
            }
            else if (_health <= maxHealth / 3 && !gameObject.GetComponent<BossMovement>().thirdStage)
            {
                gameObject.GetComponent<BossMovement>().Stage3();
            }


            damageFlash?.Flash();
            healthbar.UpdateHealthbar(_health, maxHealth);
        }
    }

    private void Die()
    {
        var position = transform.position;
        EffectManager.instance.DeathEffect(position);
        Destroy(gameObject);
        Destroy(healtbarImage);
        EffectManager.instance.CogPickup(position);
    }

    public void ActivateBoss()
    {
        canBeHarmed = true;
        healtbarImage.SetActive(true);
    }
    public void DeactivateBoss()
    {
        canBeHarmed = false;
        Invoke("HideHealth", 1f);
    }
    private void HideHealth()
    {
        healtbarImage.SetActive(false);
    }
}
