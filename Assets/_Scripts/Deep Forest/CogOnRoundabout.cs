using UnityEngine;

public class CogOnRoundabout : MonoBehaviour
{
    [SerializeField] private float speedMultiplier = 1f; 
    private float thisRotationSpeed;

    void Update()
    {
        thisRotationSpeed = -gameObject.GetComponentInParent<Roundabout>().rotationSpeed * speedMultiplier;

        transform.Rotate(new Vector3(0f, thisRotationSpeed * Time.deltaTime, 0f));
    }
}
