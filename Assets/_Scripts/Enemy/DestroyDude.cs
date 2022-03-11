using UnityEngine;

public class DestroyDude : MonoBehaviour
{
    public GameObject gate;

    private void OnDestroy()
    {
        gate.GetComponent<GateScript>().KilledAnotherOne();
    }
}
