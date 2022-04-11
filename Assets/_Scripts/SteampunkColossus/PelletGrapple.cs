using UnityEngine;

public class PelletGrapple : MonoBehaviour
{
    public Canvas grappleUI;

    private void OnMouseOver()
    {
        if (!gameObject.GetComponent<Pellet>().hit)
        {
            grappleUI.enabled = true;
            if (Input.GetKey(InputManager.instance.GrappleBtn))
            {
                Debug.Log("Nice");
            }
        }
        

    }
    private void OnMouseExit()
    {
        grappleUI.enabled = false;
    }
}
