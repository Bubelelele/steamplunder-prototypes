using UnityEngine;

public class BossStats : MonoBehaviour, IDamageable
{

    [SerializeField] private int maxHealth = 50;
    [SerializeField] private Healthbar healthbar;
    [SerializeField] private DamageFlash damageFlash;
    [SerializeField] private GameObject healtbarImage;


    private int _health;

    [HideInInspector] public bool isActive = true;

    private bool firstDone = false;
    private bool secondDone = false;

    private void Start()
    {
        _health = maxHealth;
        healthbar.UpdateHealthbar(_health, maxHealth);
    }

    public void Damage(int amount)
    {
        if (isActive)
        {
            _health -= amount;
            if (_health <= 0)
            {
                Die();
            }
            else if (_health <= maxHealth / 3 * 2 && !firstDone)
            {
                gameObject.GetComponent<BossStages>().Stage2();
                firstDone = true;
            }
            else if (_health <= maxHealth / 3 && !secondDone)
            {
                gameObject.GetComponent<BossStages>().Stage3();
                secondDone = true;
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
        isActive = true;
        healtbarImage.SetActive(true);
    }
    public void DeactivateBoss()
    {
        isActive = false;
        Invoke("HideHealth", 0.2f);
    }
    private void HideHealth()
    {
        healtbarImage.SetActive(false);
    }
}
