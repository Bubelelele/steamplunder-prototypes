using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceCog : MonoBehaviour, IInteractable
{
    public static ReplaceCog Instance;

    public GameObject cog;
    public bool haveCog;
    public Animator door;

    private void Awake()
    {
        Instance = this;
    }

    public void Interact()
    {
        if (haveCog)
        {
            cog.gameObject.SetActive(true);
            door.SetBool("OpenDoor", true);
            haveCog = false;
        }
    }

    public void StopInteract()
    {
        throw new System.NotImplementedException();
    }

    public string GetDescription()
    {
        return null;
    }
}
