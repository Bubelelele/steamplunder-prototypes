using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BlockCompletionDetector : MonoBehaviour
{
    public pressurePlateBool plateOne, plateTwo;
    [SerializeField] private UnityEvent onCompletion;

    private bool _compelted;
    
    void Start()
    {
        GetComponent<Renderer>().material.color = Color.red;
    }

    private void Update()
    {
        if (plateOne.GetPressed() && plateTwo.GetPressed())
        {
            Completed();
        }
    }

    private void Completed() {
        if (_compelted) return;
        _compelted = true;
        onCompletion.Invoke();
    }

}
