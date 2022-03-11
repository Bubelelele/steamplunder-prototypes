using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColliderTrigger : MonoBehaviour
{
    public GameObject note;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            note.SetActive(true);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            note.SetActive(false);
        }
    }
}
