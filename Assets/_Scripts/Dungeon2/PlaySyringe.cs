using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySyringe : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            transform.GetComponent<Animator>().SetTrigger("Heal");
        }
    }
}
