using System.Collections;
using UnityEngine;

public class OverworldPuzzle : MonoBehaviour
{
    [HideInInspector] public bool done = false;
    public int currentPlateToStepOn = 1;
    public GameObject[] triggers;
    public Animator energyMeterAnim;
    public Transform exit;

    public void Correct()
    {
        energyMeterAnim.SetInteger("PressurePlateNumber", currentPlateToStepOn);
        currentPlateToStepOn++;
        
        if (currentPlateToStepOn > 5)
        {
            done = true;
            StartCoroutine(Reward());
        }

    }
    public void Wrong()
    {
        for (int i = 0; i < triggers.Length; i++)
        {
            triggers[i].GetComponent<SteamPuzzleTrigger>().StartSteam();
        }

        currentPlateToStepOn = 1;
        energyMeterAnim.SetInteger("PressurePlateNumber", 0);
    }
    private IEnumerator Reward()
    {
        for (int i = 0; i < 5; i++)
        {
            EffectManager.instance.CogPickup(exit.position + Vector3.up * 2);
            yield return new WaitForSeconds(.5f);
        }
    }
}
