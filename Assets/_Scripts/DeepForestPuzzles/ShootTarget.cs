using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShootTarget : MonoBehaviour
{
    public UnityEvent whenShot;

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "EnergyBlast(Clone)")
            whenShot.Invoke();
    }
}
