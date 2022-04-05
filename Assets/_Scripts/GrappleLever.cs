using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GrappleLever : MonoBehaviour {
    
    [SerializeField] private UnityEvent onPull;
    [SerializeField] private Animator animator;
    
    public void Pull() {
        animator.SetTrigger("Pull");
        onPull.Invoke();
    }
    
}