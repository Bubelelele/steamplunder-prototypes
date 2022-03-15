using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager0 : MonoBehaviour
{
    public static PuzzleManager0 Instance;
    public bool boxInside;

    private Animator animator;

    private void Awake()
    {
        Instance = this; 
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void LiftGate()
    {
        animator.SetBool("Lift Gate", true);
    }

    public void CloseGate()
    {
        animator.SetBool("Lift Gate", false);
    }

    public void LowerBridge()
    {
        if (boxInside)
        {
            animator.SetTrigger("Lower Bridge");
        }
    }
}
