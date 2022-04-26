using UnityEngine;

public class HoldConsole : InteractableBase {

    [SerializeField] private Transform objectToRotate;

    public override void Interact() {
        objectToRotate.Rotate(Vector3.up, 1f);
    }

    public override string GetDescription() {
        return "Rotate thing";
    }
}
