using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinActivate : MonoBehaviour, IInteractable
{
    public GameObject pivot;

    private bool spun = false;
    public string GetDescription()
    {
        return "Activate Spinner";
    }

    public void Interact()
    {
        if (spun)
        {
            pivot.transform.Rotate(0, -90, 0);
            spun = !spun;
        }
        else
        {
            pivot.transform.Rotate(0, 90, 0);
            spun = !spun;
        }
      
    }

    public void StopInteract()
    {
        throw new System.NotImplementedException();
    }
}
