using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatPuzzleMaster : MonoBehaviour {

    [SerializeField] private PressurePlatePressed[] platesInSolution;

    private bool _solved;
    
    private void Start() {
        foreach (var plate in platesInSolution) {
            plate.onPressed += CheckSolution;
        }
    }

    private void CheckSolution() {
        if (_solved) return;
        
        foreach (var plate in platesInSolution) {
            if (!plate.isPressed) return;
        }

        _solved = true;
        StartCoroutine(Reward());
    }
    
    private IEnumerator Reward() {
        for (int i = 0; i < 5; i++) {
            EffectManager.instance.CogPickup(transform.position + Vector3.up * 2 + Vector3.forward * 2 + Vector3.right * 2);
            yield return new WaitForSeconds(.5f);
        }
    }
}