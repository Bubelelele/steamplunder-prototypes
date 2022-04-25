using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftDown : MonoBehaviour
{
    public Animator liftAnim;

   public void LiftDownFunction()
    {
        liftAnim.SetTrigger("LiftDown");
    }
}
