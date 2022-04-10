using UnityEngine;

public class Hotdog_Gun : MonoBehaviour
{
    
    public float damping = 10f;

    
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject parentForGun;

    private bool gunActive = false;

    void Update()
    {
        if (gunActive)
        {
            if(transform.localEulerAngles.y < 330 && transform.localEulerAngles.y > 210 && Vector3.Angle(-parentForGun.transform.right, player.transform.position - transform.position) < 60)
            {
                var targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * damping);
            }
            else if(transform.localEulerAngles.y < 210)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 211, transform.localEulerAngles.z);
            }
            else if (transform.localEulerAngles.y > 330)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 329, transform.localEulerAngles.z);
            }
        }
    }

    public void GunActive()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        gunActive = true;
    }
}
