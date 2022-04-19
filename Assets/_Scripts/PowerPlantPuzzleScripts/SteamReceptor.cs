using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SteamReceptor : MonoBehaviour, IInteractable
{
    [SerializeField] private UnityEvent onActivate;
    public float steamTimer;
    public Animator steamGauge;

        private bool hasSteamer() => GameManager.instance.player.GetComponent<GearManager>().SteamerActive;

    public string GetDescription()
    {
        if (hasSteamer())
        {
            return "use steamer";
        }
        else
            return "(require steamer)";
    }

    public void Interact()
    {
        if (hasSteamer())
        {
            SteamActivation();
            Invoke("SteamDeactivation", steamTimer);
        }
    }

    public void StopInteract()
    {
        throw new System.NotImplementedException();
    }

    private void SteamActivation()
    {
        onActivate.Invoke();
        steamGauge.SetBool("GaugeOn", true);
    }

    private void SteamDeactivation()
    {
        onActivate.Invoke();
        steamGauge.SetFloat("ResetTimer", (1/steamTimer));
        steamGauge.SetBool("GaugeOn", false);
    }
}
