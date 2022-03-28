using UnityEngine;

public class OverworldPuzzle : MonoBehaviour
{
    [HideInInspector] public bool done = false;
    public int currentPlateToStepOn = 1;
    public GameObject[] triggers;
    public Animator energyMeterAnim;

    public void Correct()
    {
        energyMeterAnim.SetInteger("PressurePlateNumber", currentPlateToStepOn);
        currentPlateToStepOn++;
        
        if (currentPlateToStepOn > 5)
        {
            done = true;
            Debug.Log("You won!");
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
}
