using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallController : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "DoorEntrance")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
