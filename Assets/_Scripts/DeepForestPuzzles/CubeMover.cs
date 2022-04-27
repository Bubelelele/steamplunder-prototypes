using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMover : MonoBehaviour
{
    public Animator move;

    public void MoveBool()
    {
        move.SetBool("MovePlatform", !move.GetBool("MovePlatform"));
    }
}
