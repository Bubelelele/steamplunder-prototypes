using UnityEngine;

public class CogLineTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MetalBoxCog")
        {
            PuzzleManager0.Instance.LowerBridge();
        }
    }
}
