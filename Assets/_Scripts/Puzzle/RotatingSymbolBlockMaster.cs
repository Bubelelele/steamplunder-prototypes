using System.Collections;
using UnityEngine;

public class RotatingSymbolBlockMaster : MonoBehaviour {

    [SerializeField] private int[] solution;
    [SerializeField] private RotatingSymbolBlock[] blocks;

    private bool _solved;

    private void Start() {
        if (solution.Length != blocks.Length) Debug.LogWarning("Rotating block puzzle solution length error");

        foreach (var block in blocks) {
            block.onRotation += CheckSolution;
        }
    }

    private void CheckSolution() {
        if (_solved) return;
        
        for (int i = 0; i < blocks.Length; i++) {
            if (blocks[i].CurrentSymbol != solution[i]) return;
        }

        _solved = true;
        StartCoroutine(Reward());
    }

    private IEnumerator Reward() {
        for (int i = 0; i < 5; i++) {
            EffectManager.instance.CogPickup(transform.position + Vector3.up * 2 + Vector3.back *4 + Vector3.right * 3.5f);
            yield return new WaitForSeconds(.5f);
        }
    }
}