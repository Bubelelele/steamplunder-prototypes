using UnityEngine;

public class RotatingSymbolBlockMaster : MonoBehaviour {

    [SerializeField] private int[] solution;
    [SerializeField] private RotatingSymbolBlock[] blocks;
    public GameObject image;

    private void Start() {
        if (solution.Length != blocks.Length) Debug.LogWarning("Rotating block puzzle solution length error");

        foreach (var block in blocks) {
            block.onRotation += CheckSolution;
        }
    }

    private void CheckSolution() {
        for (int i = 0; i < blocks.Length; i++) {
            if (blocks[i].CurrentSymbol != solution[i]) return;
        }

        image.SetActive(true);
    }
}