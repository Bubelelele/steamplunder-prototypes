using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PuzzleConsole : MonoBehaviour, IInteractable {

    [SerializeField] private CinemachineVirtualCamera virtualCam;

    public void Interact() {
        InteractionManager.instance.InteractionStarted();
        virtualCam.Priority = 12;
    }
    
    public void StopInteract() {
        InteractionManager.instance.InteractionEnded();
        virtualCam.Priority = 10;
    }

    public string GetDescription() {
        return "Interact with console";
    }
}