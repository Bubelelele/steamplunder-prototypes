using UnityEngine;

public class ValveInteract : MonoBehaviour, IInteractable
{
    public GameObject[] Steam;

    private bool SteamActive;

    private void Update()
    {
        SteamActive = Steam[0].activeSelf;
    }
    public string GetDescription()
    {
        return "Use Valve";
    }

    public void Interact()
    {
        transform.Rotate(Vector3.forward * 90);
        for (int i = 0; i < Steam.Length; i++)
        {
            Steam[i].SetActive(!Steam[i].activeSelf);
        }

    }

    public void StopInteract()
    {
        throw new System.NotImplementedException();
    }

    public bool GetSteamActive()
    {
        return SteamActive;
    }
}
