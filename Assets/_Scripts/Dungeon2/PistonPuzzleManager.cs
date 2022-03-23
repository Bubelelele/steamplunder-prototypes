using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonPuzzleManager : MonoBehaviour
{
    public static PistonPuzzleManager instance;

    public int counter = 0;

    [SerializeField] private Animator[] _pistonAnimators;
    [SerializeField] private Animator doorAnimator;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (counter == 3)
        {
            for (int i = 0; i < _pistonAnimators.Length; i++)
            {
                _pistonAnimators[i].SetBool("Retract", true);
            }
            
            doorAnimator.SetBool("OpenDoor", true);
        }
    }
}
