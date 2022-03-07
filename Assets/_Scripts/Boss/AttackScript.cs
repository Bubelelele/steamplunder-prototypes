using UnityEngine;
using Random = UnityEngine.Random;

public class AttackScript : MonoBehaviour
{
    [SerializeField] private GameObject detectionTrigger;
    [SerializeField] private GameObject bossCart;
    [SerializeField] private Animator bossAnim;

    private bool animationIsPlaying = false;

    private void Update()
    {

        if (detectionTrigger.GetComponent<BossDetectionTrigger>().attackRange)
        {
            if (!animationIsPlaying)
            {
                bossCart.GetComponent<BossMovement>().DontWalkToPlayer();

                int whichAttack = Random.Range(0, 4);
                Debug.Log(whichAttack);

                if (whichAttack == 0)
                {
                    SlashSpree();
                }
                else if (whichAttack == 1)
                {
                    Block();
                }
                else if (whichAttack == 2)
                {
                    Punch();
                }
                else if (whichAttack == 3)
                {
                    SingleSlash();
                }
                animationIsPlaying = true;
            }
        }
    }
    public void SlashSpree()
    {
        bossAnim.SetTrigger("SlashSpree");
    }
    public void Block()
    {
        bossAnim.SetBool("Block", true);
        Invoke("Slash", Random.Range(5, 10f)*0.8f);
    }
    public void Slash()
    {
        bossAnim.SetTrigger("Slash");
    }
    public void Punch()
    {
        int punchChance = Random.Range(0,3);

        if(punchChance == 0)
        {
            bossAnim.SetInteger("PunchInt", 1);
        }
        else if (punchChance == 1)
        {
            bossAnim.SetInteger("PunchInt", 2);
        }
        else
        {
            bossAnim.SetInteger("PunchInt", 3);
        }
    }
    public void SingleSlash()
    {
        bossAnim.SetTrigger("SingleSlash");
    }

    public void Stunned()
    {
        bossAnim.SetBool("Stunned", true);
    }
    public void Shield()
    {
        gameObject.GetComponent<BossStats>().CannotBeHarmed();
    }
    public void NoShield()
    {
        bossAnim.SetBool("Block", false);
        gameObject.GetComponent<BossStats>().CanBeHarmed();
        
    }
    public void ActionOver()
    {

        animationIsPlaying = false;
        bossAnim.SetBool("Block", false);
        bossAnim.SetBool("Stunned", false);
        bossAnim.SetInteger("PunchInt", 0);
        bossCart.GetComponent<BossMovement>().WalkToPlayer();
    }

}
