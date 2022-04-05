using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pressurePlateBool : MonoBehaviour
{
    private bool pressed = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MovingBox" || other.gameObject.tag == "Player")
        {
            pressed = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "MovingBox" || other.gameObject.tag == "Player")
        {
            pressed = false;
        }
    }

    public bool GetPressed()
    {
        return pressed;
    }
}
