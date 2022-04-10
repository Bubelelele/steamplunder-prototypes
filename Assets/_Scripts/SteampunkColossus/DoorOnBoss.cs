using UnityEngine;

public class DoorOnBoss : MonoBehaviour
{
    public GameObject bossBody;
    public GameObject DoorToDragOff;
    public ParticleSystem explotion;
    public Canvas grappleUI;

    private bool draggedOff = false;
    private bool canBeDraggedOff = false;

    private void OnMouseOver()
    {
        if (!draggedOff && canBeDraggedOff &&bossBody.GetComponent<SC_Stats>().isActive)
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
        explotion.Play();
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

    public void CanBeDraggedOff()
    {
        canBeDraggedOff = true;
    }
}
