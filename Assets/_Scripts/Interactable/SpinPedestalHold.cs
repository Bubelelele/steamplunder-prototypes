using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpinPedestalHold : InteractableBase {

    [SerializeField] private bool canOnlyActivateOnce;
    [SerializeField] private UnityEvent onActivate;
    [SerializeField] private Animator spinAnim;

    private bool _activated;
    
    public override void Interact() {
        if (!CheckSpinActive() || _activated) return;
        
        spinAnim.SetTrigger("interacted");
        onActivate?.Invoke();
        if (canOnlyActivateOnce) _activated = true;
    }

    public override string GetDescription() {
        return CheckSpinActive()
            ? "Spin with Axe"
            : "(Spinning Axe required)";
    }

    private bool CheckSpinActive() => GameManager.instance.player.GetComponent<GearManager>().SpinActive;
}