using UnityEngine;

public class UI_Showcase : MonoBehaviour
{
    public ParticleSystem explotion;
    public GameObject door;
    public Canvas grappleUI;
    public Canvas infoUI;

    private bool draggedOff = false;

    private void OnMouseOver()
    {
        if (!draggedOff)
        {
            grappleUI.enabled = true;
            infoUI.enabled = true;
            if (Input.GetKey(InputManager.instance.GrappleBtn))
            {
                DragOff();                
            }
        }
        
    }
    private void OnMouseExit()
    {
        grappleUI.enabled = false;
        infoUI.enabled = false;
    }
    private void DragOff()
    {
        explotion.Play();
        draggedOff = true;
        infoUI.enabled = false;
        grappleUI.enabled = false;
        door.SetActive(false);
    }
}
