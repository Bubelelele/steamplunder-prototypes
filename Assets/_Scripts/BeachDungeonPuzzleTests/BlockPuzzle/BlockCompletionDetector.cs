using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCompletionDetector : MonoBehaviour
{
    public pressurePlateBool plateOne, plateTwo;
    void Start()
    {
        GetComponent<Renderer>().material.color = Color.red;
    }

    private void Update()
    {
        if (plateOne.GetPressed() && plateTwo.GetPressed())
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
    }

}
