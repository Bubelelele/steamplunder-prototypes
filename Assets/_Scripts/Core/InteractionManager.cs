using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour {
    public static InteractionManager instance;
    private void Awake() => instance = this;

    [SerializeField] private GameObject interactionUI;
    
    private IInteractable _currentInteraction;
    
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.instance.state == GameState.Interaction) 
            _currentInteraction?.StopInteract();
    }
    
    public void InteractionStarted() {
        GameManager.instance.UpdateGameState(GameState.Interaction);
        interactionUI.SetActive(true);
    }
    
    public void InteractionEnded() {
        GameManager.instance.UpdateGameState(GameState.Default);
        interactionUI.SetActive(false);
    }

    public void SetCurrentInteraction(IInteractable interactable) => _currentInteraction = interactable;

}