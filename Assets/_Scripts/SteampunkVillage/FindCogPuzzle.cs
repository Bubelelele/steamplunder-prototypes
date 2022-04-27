using System.Collections;
using UnityEngine;

public class FindCogPuzzle : MonoBehaviour, IInteractable
{
    private bool hasCog = false;
    public GameObject Cog, dropCog;
    public string GetDescription()
    {
        if (hasCog)
            return "Place Cog";
        else
            return "[Cog is missing]";
    }

    public void Interact()
    {
        if (hasCog)
        {
            Cog.SetActive(true);
            dropCog.GetComponent<DropCogs>().Correct();
        }
        else
        {
            Debug.Log("has no cog");
        }

    }

    public void StopInteract()
    {
        throw new System.NotImplementedException();
    }

    public void GetCog()
    {
        hasCog = true;
    }
}
