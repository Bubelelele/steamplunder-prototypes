using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable {
    [SerializeField] private int index = 0;
    [SerializeField] private string bruh;
    
    public void Interact() {
        GameManager.instance.LoadScene(index);
    }

    public void StopInteract() {
        throw new System.NotImplementedException();
    }

    public string GetDescription() {
        return bruh;
    }
}