using UnityEngine;
using UnityEngine.Events;

public class FindCogMachine : MonoBehaviour, IInteractable
{
    private bool hasCog = false;
    public GameObject Cog;
    [SerializeField] private UnityEvent onActivate;
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
            onActivate.Invoke();
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