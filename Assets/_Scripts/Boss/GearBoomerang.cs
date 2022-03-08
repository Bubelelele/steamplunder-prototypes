using UnityEngine;

public class GearBoomerang : MonoBehaviour
{
    [SerializeField] private Transform center;
    [SerializeField] private Transform originalRotation;
    [SerializeField] private Transform gearPosition;
    [SerializeField] private GameObject gearInGauntlet;

    [SerializeField] private float rotationSpeed = 500f;

    
    private bool rotateOnce;

    private void Update()
    {
        
        
        if (rotateOnce)
        {
            center.transform.Rotate(-rotationSpeed * Time.deltaTime,0f, 0f);
            if (center.transform.eulerAngles.y >= originalRotation.transform.eulerAngles.y && center.transform.eulerAngles.y <= originalRotation.transform.eulerAngles.y + 7.5f)
            {
                rotateOnce = false;
                gameObject.transform.position = gearPosition.position;
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                gearInGauntlet.SetActive(true);
                gameObject.GetComponent<Collider>().enabled = false;
                AudioManager.instance.Play("cogpickup");
            }
        }

    }
    public void ActivateBoomerang()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        gameObject.GetComponent<Collider>().enabled = true;
        //Calculating middle point
        center.position = (GameManager.instance.player.transform.position + gameObject.transform.position) * 0.5f;
        gameObject.transform.parent = center;
        originalRotation.rotation = center.transform.rotation;
        rotateOnce = true;
        gearInGauntlet.SetActive(false);
        AudioManager.instance.Play("hitshield");
    }
}
