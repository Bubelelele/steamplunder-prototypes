using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    [SerializeField] private Animator bossAnim;

    public void Block()
    {
        bossAnim.SetBool("Block", true);
    }
    public void UnBlock()
    {
        bossAnim.SetBool("Block", false);
    }
    public void Slash()
    {
        bossAnim.SetBool("Slash", true);
    }
    public void NoSlash()
    {
        bossAnim.SetBool("Slash", false);
    }
}
