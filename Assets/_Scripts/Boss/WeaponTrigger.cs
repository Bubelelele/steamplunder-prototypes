using UnityEngine;

public class WeaponTrigger : MonoBehaviour
{
    public GameObject boss;



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && boss.GetComponent<AttackScript>().leathal)
        {
            EffectManager.instance.BloodSplat(other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(gameObject.transform.position));
            other.GetComponent<PlayerStats>().Damage(boss.GetComponent<AttackScript>().attackDamage);   
        }
    }

}
