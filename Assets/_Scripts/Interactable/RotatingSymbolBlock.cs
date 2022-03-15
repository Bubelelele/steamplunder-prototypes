using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class RotatingSymbolBlock : MonoBehaviour, IInteractable {
    
    public int CurrentSymbol { get; private set; } = 1;
    // 1-Clubs, 2-Diamonds, 3-Hearts, 4-Spades //
    public event Action onRotation;
    
    [SerializeField] private float rotateDelay = .01f;
    [SerializeField] private Transform blockTransform;
    [SerializeField] private bool balls;
    
    
    private bool _isRotating;

    private void Start() {
        //Random start symbol
        if (balls) return;
        CurrentSymbol = Random.Range(1, 5);
        blockTransform.Rotate(new Vector3(0, (CurrentSymbol-1)*90f));
    }

    public void Interact() {
        if (_isRotating) return;
        
        _isRotating = true;
        StartCoroutine(Rotate());
    }

    public void StopInteract() { }

    public string GetDescription() {
        return _isRotating ? "(Rotating)" : "Rotate Block";
    }

    private IEnumerator Rotate() {
        for (int i = 0; i < 90; i++) {
            blockTransform.Rotate(new Vector3(0, 1));
            yield return new WaitForSeconds(rotateDelay);
        }

        CurrentSymbol++;
        if (CurrentSymbol > 4) CurrentSymbol = 1;
        onRotation?.Invoke();
        _isRotating = false;
    }
}