
using UnityEngine;

public class LiftDown : MonoBehaviour
{
    public Animator[] liftAnim;

   public void LiftDownFunction()
    {
        for (int i = 0; i < liftAnim.Length; i++)
        {
            liftAnim[i].SetTrigger("IsActive");
        }
        
    }
}
