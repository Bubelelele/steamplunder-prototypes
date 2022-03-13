using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceCog : MonoBehaviour, IInteractable
{
    public static ReplaceCog Instance;

    public GameObject cog;
    public bool haveCog;

    private void Awake()
    {
        Instance = this;
    }

    public void Interact()
    {
        if (haveCog)
        {
            cog.gameObject.SetActive(true);
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
