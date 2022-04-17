using System;
using UnityEngine;
using UnityEngine.Playables;

public class Minecart : MonoBehaviour, IInteractable {

    [SerializeField] private PlayableDirector[] cutscenes;
    [SerializeField] private Spin[] wheelSpin;

    public int nextCheckpoint;
    
    private int _currentCheckpoint;

    private void Start() {
        foreach (var cutscene in cutscenes) {
            cutscene.stopped += StopWheelSpin;
        }
    }

    public void Interact() {
        if (StandStill()) return;

        if (cutscenes.Length >= nextCheckpoint) {
            cutscenes[nextCheckpoint-1].Play();
            _currentCheckpoint = nextCheckpoint;
            StartWheelSpin();
        }
    }

    public void StopInteract() {
        throw new System.NotImplementedException();
    }

    public string GetDescription() {
        return StandStill() ? "(Path ahead seems to be blocked)" : "Get into mine cart";
    }

    private bool StandStill() => nextCheckpoint == _currentCheckpoint;

    public void Next() => nextCheckpoint++;
    
    private void StopWheelSpin(PlayableDirector obj) {
        foreach (var spin in wheelSpin) {
            spin.speed = 0;
        }
    }

    private void StartWheelSpin() {
        foreach (var spin in wheelSpin) {
            spin.speed = 5f;
        }
    }
}