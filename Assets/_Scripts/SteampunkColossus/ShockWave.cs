using UnityEngine;

public class ShockWave : MonoBehaviour
{
    public int shockwaveDamage = 10;

    private Animator bossAnim;

    private void Start()
    {
        bossAnim = GameObject.Find("BossAnimator").GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !gameObject.GetComponentInParent<PelletGrapple>().canHurtBoss)
        {
            other.GetComponent<PlayerStats>().Damage(shockwaveDamage);
            EffectManager.instance.BloodSplat(other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(gameObject.transform.position));
            gameObject.GetComponentInParent<PushBackPlayer>().PushBack(3);
        }
        if (other.gameObject.tag == "Boss" && gameObject.GetComponentInParent<PelletGrapple>().canHurtBoss)
        {
            if(other.GetComponent<SC_Stats>() != null)
            {
                other.GetComponent<SC_Stats>().Damage(100);
                bossAnim.SetBool("IsDown", true);
                bossAnim.SetBool("Shoot", false);
            }
        }
    }
}
