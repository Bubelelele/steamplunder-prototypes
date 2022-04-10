using UnityEngine;

public class DoorOnBoss : MonoBehaviour
{
    public GameObject bossBody;
    public GameObject DoorToDragOff;
    public Canvas grappleUI;

    private bool draggedOff = false;

    private void OnMouseOver()
    {
        if (!draggedOff && bossBody.GetComponent<SC_Stats>().isActive)
        {
            grappleUI.enabled = true;
            if (Input.GetKey(InputManager.instance.GrappleBtn))
            {
                DragOff();                
            }
        }
        
    }
    private void OnMouseExit()
    {
        grappleUI.enabled = false;
    }
    private void DragOff()
    {
        draggedOff = true;
        grappleUI.enabled = false;
        DoorToDragOff.transform.parent = null;
        DoorToDragOff.AddComponent<BoxCollider>();
        DoorToDragOff.AddComponent<Rigidbody>();
        DoorToDragOff.GetComponent<Rigidbody>().AddRelativeForce(Vector3.right * -10f, ForceMode.Impulse);
        Invoke("Hide", 2f);
    }

    private void Hide()
    {
        DoorToDragOff.GetComponent<MeshRenderer>().enabled = false;
    }
}
