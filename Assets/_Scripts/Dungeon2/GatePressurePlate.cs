using UnityEngine;

public class GatePressurePlate : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PuzzleManager0.Instance.LiftGate();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PuzzleManager0.Instance.CloseGate();
    }
}
