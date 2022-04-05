using System.Collections;
using UnityEngine;

public class RotationCompletionDetection : MonoBehaviour
{

    [SerializeField] private int[] solution;
    [SerializeField] private RotatingSymbolBlock[] blocks;

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

        StartCoroutine(Reward());
    }

    private IEnumerator Reward()
    {
        GetComponent<Renderer>().material.color = Color.green;
        yield return new WaitForSeconds(.5f);
    }
}