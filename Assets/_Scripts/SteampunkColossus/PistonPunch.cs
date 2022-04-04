using UnityEngine;

public class PistonPunch : MonoBehaviour
{
    public bool pistonPunch = true;

    [SerializeField] private GameObject player;
    [SerializeField] private float damping = 20;


    private void Update()
    {
        if (pistonPunch)
        {
            var targetRotation = Quaternion.LookRotation(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z) - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * damping);
        }
    }

    public void TurnOnPistonPunch() { pistonPunch = true; }
    public void TurnOffPistonPunch()
    { 
        pistonPunch = false;
        transform.localRotation = Quaternion.Euler(15f, 180f, 0f);
    }
}
