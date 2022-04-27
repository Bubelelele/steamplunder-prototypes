using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    private bool inputOne, inputTwo;
    public GameObject cannonBall, destroyableDoor;
    public Animator cannonFire;

    void Update()
    {
        if (inputOne && inputTwo)
        {
            cannonFire.SetTrigger("FireCannon");
        }
    }

    public void ActivateInputOne()
    {
        inputOne = !inputOne;
    }

    public void ActivateInputTwo()
    {
        inputTwo = !inputTwo;
    }

    public void DestroyBallAndDoor()
    {
        Destroy(cannonBall);
        Destroy(destroyableDoor);
    }
}
