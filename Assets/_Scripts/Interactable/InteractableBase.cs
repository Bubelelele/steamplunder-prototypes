using UnityEngine;

public abstract class InteractableBase : MonoBehaviour {
    
    public KeyCode InteractKey = KeyCode.E;
    public bool holdToInteract;
    
    public abstract void Interact();
    
    public abstract string GetDescription();
    
}