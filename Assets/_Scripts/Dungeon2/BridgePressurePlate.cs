using UnityEngine;

public class BridgePressurePlate : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MovingBox")
        {
            PuzzleManager0.Instance.boxInside = true;
        }
    }
}
