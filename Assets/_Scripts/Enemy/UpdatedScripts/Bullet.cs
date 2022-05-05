using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int damage;
    private float bulletSpeed;

    private void Start()
    {
        Destroy(gameObject, 2f);

    }
    private void FixedUpdate()
    {
        transform.position += transform.forward * bulletSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerStats>().Damage(damage);
            EffectManager.instance.BloodSplat(other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(gameObject.transform.position));
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "Enemy")
        {
            other.GetComponent<EnemyStats>().Damage(damage);
            EffectManager.instance.BloodSplat(other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(gameObject.transform.position));
            Destroy (gameObject);
        }
    }
    public void SetDamage(int dmg)
    {
        damage = dmg;
    }
    public void SetBulletSpeed(float speed)
    {
        bulletSpeed = speed;
    }
}
