using System;
using UnityEngine;

public class PressurePlatePressed : MonoBehaviour {
    public bool isPressed;

    public event Action onPressed; 

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("MovingBox") || other.CompareTag("Player")) {
            isPressed = true;
            onPressed?.Invoke();
        }
    }
    
    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("MovingBox") || other.CompareTag("Player")) {
            isPressed = false;
        }
    }
}