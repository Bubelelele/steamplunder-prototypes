using UnityEngine;

public class SwordTrigger : MonoBehaviour
{
    public GameObject enemy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && enemy.GetComponent<MeleeEnemy>().lethal)
        {
            EffectManager.instance.BloodSplat(other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(gameObject.transform.position));
            other.GetComponent<PlayerStats>().Damage(enemy.GetComponent<MeleeEnemy>().attackDamage);
            enemy.GetComponent<PushBackPlayer>().PushBack(3);

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
