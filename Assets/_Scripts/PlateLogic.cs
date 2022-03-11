using UnityEngine;

public class PlateLogic : MonoBehaviour
{
    public bool isPressed = false;
    
    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            isPressed = !isPressed;
        }
    }
}
