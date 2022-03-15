using UnityEngine;

public class HeavyWeaponTrigger : MonoBehaviour
{
    [SerializeField] private GameObject heavyEnemyCart;
    [SerializeField] private GameObject heavyEnemy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && heavyEnemy.GetComponent<HeavyEnemyStats>().leathal)
        {
            EffectManager.instance.BloodSplat(other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(gameObject.transform.position));
            other.GetComponent<PlayerStats>().Damage(heavyEnemyCart.GetComponent<AIHeavy>().attackDamage);
            
            int soundNumber = Random.Range(0, 3);
            if (soundNumber == 0)
            {
                AudioManager.instance.Play("blood1");
            }
            else if (soundNumber == 1)
            {
                AudioManager.instance.Play("blood2");
            }
            else
            {
                AudioManager.instance.Play("blood3");
            }
        }
    }

}
