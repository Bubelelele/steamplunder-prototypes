using UnityEngine;

public class Roundabout : MonoBehaviour
{
    [HideInInspector] public float rotationSpeed = 0f;

    [SerializeField] private GameObject activationTrigger;

    void Update()
    {
        if (Input.GetKey(KeyCode.E) && activationTrigger.GetComponent<ActiviationTrigger>().canSpin){   rotationSpeed = -50f;}
        else{   rotationSpeed = 0f;}

        transform.Rotate(new Vector3(0f, rotationSpeed * Time.deltaTime, 0f));
    }
}
