using UnityEngine;

public class PistonPunch : MonoBehaviour
{
    public bool pistonPunch = true;

    [SerializeField] private GameObject player;
    [SerializeField] private float damping = 8;


    private void Update()
    {
        if (pistonPunch)
        {
            var targetRotation = Quaternion.LookRotation(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z) - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * damping);

            if (gameObject.name == "PistonPunshRight" && (transform.localEulerAngles.y > 270 || transform.localEulerAngles.y < 135))
            {
                damping = 0;
            }
            else if (gameObject.name == "PistonPunshLeft" && (transform.localEulerAngles.y > 225 || transform.localEulerAngles.y < 90))
            {
                damping = 0;
            }
            else if (gameObject.name == "PistonPunshLeft" || gameObject.name == "PistonPunshRight")
            {
                damping = 8;
            }
        }
    }

    public void TurnOnPistonPunch() { pistonPunch = true; }
    public void TurnOffPistonPunch()
    { 
        pistonPunch = false;
        transform.localRotation = Quaternion.Euler(15f, 180f, 0f);
    }
}
