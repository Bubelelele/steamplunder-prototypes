using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class RotationCompletionDetection : MonoBehaviour
{

    [SerializeField] private int[] solution;
    [SerializeField] private RotatingSymbolBlock[] blocks;
    [SerializeField] private UnityEvent onCompletion;
    private bool _compelted;


    private void Start()
    {
        if (solution.Length != blocks.Length) Debug.LogWarning("Rotating block puzzle solution length error");
        GetComponent<Renderer>().material.color = Color.red;

        foreach (var block in blocks)
        {
            block.onRotation += CheckSolution;
        }
    }

    private void CheckSolution()
    {
        for (int i = 0; i < blocks.Length; i++)
        {
            if (blocks[i].CurrentSymbol != solution[i]) return;
        }

        Completed();
    }

    private void Completed() {
        if (_compelted) return;
        _compelted = true;
        onCompletion.Invoke();
    }
    
}