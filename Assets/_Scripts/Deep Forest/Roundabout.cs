using UnityEngine;

public class Roundabout : MonoBehaviour
{
    public GameObject platformToSpin;
    [HideInInspector] public float rotationSpeed = 0f;

    [SerializeField] private GameObject activationTrigger;

    void Update()
    {
        if (Input.GetKey(KeyCode.E) && activationTrigger.GetComponent<ActiviationTrigger>().canSpin){ SetSpeed(); }
        else{   rotationSpeed = 0f;}

        platformToSpin.transform.Rotate(new Vector3(0f, rotationSpeed * Time.deltaTime, 0f));
    }

    public void SetSpeed()
    {
        rotationSpeed = -50f;
    }
}
