using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeTerminal : MonoBehaviour
{
    public Animator[] liftAnim;
    
    public void Init() => StartCoroutine(rocks());
    
    private IEnumerator rocks()
    {
        for (int i = 0; i < liftAnim.Length; i++)
        {
            liftAnim[i].SetBool("LiftUp", true);
            yield return new WaitForSeconds(1f);
        }
    }
}
