using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeTerminal : MonoBehaviour, IInteractable
{
    public Animator[] liftAnim;
    public string GetDescription()
    {
        return "Use axe on terminal";
    }

    public void Interact()
    {
        StartCoroutine(rocks());
    }

    public void StopInteract()
    {
        throw new System.NotImplementedException();
    }

    private IEnumerator rocks()
    {
        for (int i = 0; i < liftAnim.Length; i++)
        {
            liftAnim[i].SetBool("LiftUp", true);
            yield return new WaitForSeconds(1f);
        }
    }
}
