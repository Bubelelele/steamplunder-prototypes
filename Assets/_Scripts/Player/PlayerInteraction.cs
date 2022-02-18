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

            if (interactable != null) {
                hitSomething = true;
                interactionText.text = interactable.GetDescription();
                interactionIndicatorUI.transform.position = mainCam.WorldToScreenPoint(hit.collider.transform.position);

                if (Input.GetKeyDown(InputManager.instance.InteractBtn)) {
                    interactable.Interact();
                    interactionIndicatorUI.SetActive(false);
                    InteractionManager.instance.SetCurrentInteraction(interactable);
                    return;
                }
            }
        }
        
        interactionIndicatorUI.SetActive(hitSomething);
    }
}