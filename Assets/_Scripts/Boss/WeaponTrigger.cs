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
            
            if (boss.GetComponent<AttackScript>().pushBack)
            {
                boss.GetComponent<PushBackPlayer>().PushBack(6);
            }
            else
            {
                boss.GetComponent<PushBackPlayer>().PushBack(3);
            }
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
