using UnityEngine;

public class TriggerForWeapon : MonoBehaviour
{
    public GameObject bossBody;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && bossBody.GetComponent<SC_AttackScript>().leathal)
        {
            EffectManager.instance.BloodSplat(other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(gameObject.transform.position));
            other.GetComponent<PlayerStats>().Damage(bossBody.GetComponent<SC_AttackScript>().attackDamage);
            bossBody.GetComponent<PushBackPlayer>().PushBack(8);
            
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
