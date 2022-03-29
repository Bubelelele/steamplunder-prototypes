using UnityEngine;

public class UIPopup : MonoBehaviour, IInteractable
{
    public string GetDescription()
    {
        return "Hold";
    }
    public void Interact() { }//Not used
    public void StopInteract() { }//Not used
}
