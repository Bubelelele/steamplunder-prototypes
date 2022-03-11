using UnityEngine;

public class GearRotation : MonoBehaviour
{
    public float rotationSpeed = 20f;
    void Update()
    {
        transform.Rotate(Time.deltaTime * rotationSpeed, 0f, 0f, Space.Self);
    }
}
