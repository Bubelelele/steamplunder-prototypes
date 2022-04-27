using TMPro;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {

    [SerializeField] private Camera mainCam;
    [SerializeField] private float interactionDistance = 2f;

    [SerializeField] private GameObject interactionIndicatorUI;
    [SerializeField] private TextMeshProUGUI interactionText;

    private void LateUpdate() {
        InteractionRay();
    }

    private void InteractionRay() {
        Ray ray = new Ray(transform.position, transform.forward);
        bool hitSomething = false;

        if (Physics.Raycast(ray, out var hit, interactionDistance)) {
            var interactable = hit.collider.GetComponent<IInteractable>();
            var holdInteractable = hit.collider.GetComponent<InteractableBase>();

            if (interactable != null) {
                hitSomething = true;
                interactionText.text = interactable.GetDescription();
                interactionIndicatorUI.transform.position = mainCam.WorldToScreenPoint(hit.collider.transform.position);

                if (Input.GetKeyDown(InputManager.instance.InteractBtn) && InteractionManager.instance.CanInteract) {
                    interactable.Interact();
                    interactionIndicatorUI.SetActive(false);
                    InteractionManager.instance.SetCurrentInteraction(interactable);
                    return;
                }
            }
            
            else if (holdInteractable != null) {
                hitSomething = true;
                interactionText.text = holdInteractable.GetDescription();
                interactionIndicatorUI.transform.position = mainCam.WorldToScreenPoint(hit.collider.transform.position);

                if (Input.GetKey(InputManager.instance.InteractBtn)) {
                    holdInteractable.Interact();
                    return;
                }
            }
        }
        
        interactionIndicatorUI.SetActive(hitSomething);
    }
}