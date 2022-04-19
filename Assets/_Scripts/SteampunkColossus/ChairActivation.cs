using UnityEngine;

public class ChairActivation : MonoBehaviour, IInteractable
{
    public Animator bossAnim;

    private bool activated = false;

    public string GetDescription()
    {
        return "";
    }

    public void Interact()
    {
        if (!activated)
        {
            bossAnim.SetTrigger("Activate");
            activated = true;
            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        }

    }
    public void StopInteract()
    {
        //Not used
    }
}
