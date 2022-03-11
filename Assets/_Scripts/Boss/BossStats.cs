using UnityEngine;

public class BossStats : MonoBehaviour, IDamageable
{

    [SerializeField] private int maxHealth = 50;
    [SerializeField] private Healthbar healthbar;
    [SerializeField] private DamageFlash damageFlash;
    [SerializeField] private GameObject healtbarImage;
    [SerializeField] private GameObject bossCart;
    [SerializeField] private ParticleSystem spark;


    private int _health;

    [HideInInspector] public bool isActive = true;
    [HideInInspector] public bool canBeHarmed = true;

    private bool firstDone = false;
    private bool secondDone = false;

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
            else if (_health <= maxHealth / 3 * 2 && !firstDone)
            {
                bossCart.GetComponent<BossStages>().Stage2();
                firstDone = true;
            }
            else if (_health <= maxHealth / 3 && !secondDone)
            {
                bossCart.GetComponent<BossStages>().Stage3();
                secondDone = true;
            }


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
        Destroy(gameObject);
        Destroy(healtbarImage);
        EffectManager.instance.CogPickup(position);
    }

    public void ActivateBoss()
    {
        isActive = true;
        CanBeHarmed();
        healtbarImage.SetActive(true);
    }
    public void DeactivateBoss()
    {
        isActive = false;
        CannotBeHarmed();
        gameObject.GetComponent<AttackScript>().Abort();
        gameObject.GetComponent<AttackScript>().ActionOver();
        Invoke("HideHealth", 0.2f);
    }
    private void HideHealth()
    {
        healtbarImage.SetActive(false);
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
