using UnityEngine;

public class FindCogMachine : MonoBehaviour, IInteractable
{
    private bool hasCog = false;
    public GameObject Cog, cube;
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
            cube.GetComponent<Renderer>().material.color = Color.green;
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

    private void Start()
    {
        cube.GetComponent<Renderer>().material.color = Color.red;
    }
}
