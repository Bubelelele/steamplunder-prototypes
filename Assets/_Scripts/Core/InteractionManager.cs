using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour {
    public static InteractionManager instance;
    private void Awake() => instance = this;

    [SerializeField] private GameObject interactionUI;

    public bool CanInteract { get; private set; } = true;
    
    private IInteractable _currentInteraction;
    
    private void Update() {
        if (Input.GetKeyDown(InputManager.instance.InteractBtn) && GameManager.instance.state == GameState.Interaction) {
            _currentInteraction?.StopInteract();
            CanInteract = false;
            StartCoroutine(CanInteractDelay());
        }
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

    private IEnumerator CanInteractDelay() {
        yield return new WaitForSeconds(.5f);
        CanInteract = true;
    }

}