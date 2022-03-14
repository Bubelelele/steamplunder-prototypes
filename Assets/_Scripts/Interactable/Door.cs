using UnityEngine;

public class Door : MonoBehaviour, IInteractable {
    [SerializeField] private int buildIndex;
    [SerializeField] private string descriptionText;
    
    public void Interact() {
        GameManager.instance.LoadScene(buildIndex);
    }

    public void StopInteract() {
        throw new System.NotImplementedException();
    }

    public string GetDescription() {
        return descriptionText;
    }
}