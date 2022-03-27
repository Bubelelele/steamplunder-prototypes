using UnityEngine;

public class Roundabout : MonoBehaviour
{
    [HideInInspector] public float rotationSpeed = 0f;

    void Update()
    {
        if (Input.GetKey(KeyCode.E)){   rotationSpeed = -20f;}
        else{   rotationSpeed = 0f;}

        transform.Rotate(new Vector3(0f, rotationSpeed * Time.deltaTime, 0f));
    }
}
