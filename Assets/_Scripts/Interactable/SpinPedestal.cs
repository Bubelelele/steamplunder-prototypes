using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpinPedestal : MonoBehaviour, IInteractable {

    [SerializeField] private bool canOnlyActivateOnce;
    [SerializeField] private UnityEvent onActivate;
    [SerializeField] private Animator spinAnim;

    private bool _activated;
    
    public void Interact() {
        if (!CheckSpinActive() || _activated) return;
        
        spinAnim.SetTrigger("interacted");
        onActivate?.Invoke();
        if (canOnlyActivateOnce) _activated = true;
    }

    public void StopInteract() {
        throw new System.NotImplementedException();
    }

    public string GetDescription() {
        string msg = "(Spinning Axe required)";
        if (_activated) msg = "(Cannot be activated again)";
        else if (CheckSpinActive()) msg = "Spin with Axe";

        return msg;
    }

    private bool CheckSpinActive() => GameManager.instance.player.GetComponent<GearManager>().SpinActive;
}