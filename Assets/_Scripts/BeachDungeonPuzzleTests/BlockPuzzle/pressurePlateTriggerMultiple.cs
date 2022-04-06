using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pressurePlateTriggerMultiple : MonoBehaviour
{
    public Animator[] liftAnim;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MovingBox" || other.gameObject.tag == "Player")
        {
            for (int i = 0; i < liftAnim.Length; i++)
            {
                liftAnim[i].SetBool("LiftUp", true);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "MovingBox" || other.gameObject.tag == "Player")
        {
            for (int i = 0; i < liftAnim.Length; i++)
            {
                liftAnim[i].SetBool("LiftUp", false);
            }
        }
    }
}
