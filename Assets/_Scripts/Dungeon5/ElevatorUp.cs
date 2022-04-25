using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorUp : MonoBehaviour
{
    public Animator liftAnim;
 
    public void Lift()
    {
        liftAnim.SetTrigger("Lifting");
    }
}
