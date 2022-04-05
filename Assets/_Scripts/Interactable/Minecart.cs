using UnityEngine;
using UnityEngine.Playables;

public class Minecart : MonoBehaviour, IInteractable {

    [SerializeField] private PlayableDirector[] cutscenes;

    public int nextCheckpoint;
    
    private int _currentCheckpoint;

    public void Interact() {
        if (StandStill()) return;

        if (cutscenes.Length >= nextCheckpoint) {
            cutscenes[nextCheckpoint-1].Play();
            _currentCheckpoint = nextCheckpoint;
        }
    }

    public void StopInteract() {
        throw new System.NotImplementedException();
    }

    public string GetDescription() {
        return StandStill() ? "(Path ahead seems to be blocked)" : "Get into mine cart";
    }

    private bool StandStill() => nextCheckpoint == _currentCheckpoint;
}