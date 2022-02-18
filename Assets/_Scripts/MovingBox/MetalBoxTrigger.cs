using UnityEngine;

public class MetalBoxTrigger : MonoBehaviour
{
    public GameObject movingBox;
    public GameObject player;


    private void OnTriggerEnter(Collider other)
    {
        //Detects which direction the box should move based on which side the player is on

        if (other.gameObject == GameManager.instance.player.gameObject)
        {
            if (gameObject.name == "Up")
            {
                movingBox.GetComponent<MetalBox>().DownDir();
            }
            else if (gameObject.name == "Down")
            {
                movingBox.GetComponent<MetalBox>().UpDir();
            }
            else if (gameObject.name == "Left")
            {
                movingBox.GetComponent<MetalBox>().RightDir();
            }
            else if (gameObject.name == "Right")
            {
                movingBox.GetComponent<MetalBox>().LeftDir();
            }
        }


    }
    private void OnTriggerStay(Collider other)
    {
        if ((other.gameObject.tag == "TopEdge" || other.gameObject.tag == "MovingBox") && gameObject.name == "Up") { movingBox.GetComponent<MetalBox>().TopEdge(); }
        if ((other.gameObject.tag == "BottomEdge" || other.gameObject.tag == "MovingBox") && gameObject.name == "Down") { movingBox.GetComponent<MetalBox>().BottomEdge(); }
        if ((other.gameObject.tag == "LeftEdge" || other.gameObject.tag == "MovingBox") && gameObject.name == "Left") { movingBox.GetComponent<MetalBox>().LeftEdge(); }
        if ((other.gameObject.tag == "RightEdge" || other.gameObject.tag == "MovingBox") && gameObject.name == "Right") { movingBox.GetComponent<MetalBox>().RightEdge(); }


        if (other.gameObject == GameManager.instance.player.gameObject) { movingBox.GetComponent<MetalBox>().CanSlide(); }
    }
    private void OnTriggerExit(Collider other)
    {
        if ((other.gameObject.tag == "TopEdge" || other.gameObject.tag == "MovingBox") && gameObject.name == "Up") { movingBox.GetComponent<MetalBox>().NotTopEdge(); }
        if ((other.gameObject.tag == "BottomEdge" || other.gameObject.tag == "MovingBox") && gameObject.name == "Down") { movingBox.GetComponent<MetalBox>().NotBottomEdge(); }
        if ((other.gameObject.tag == "LeftEdge" || other.gameObject.tag == "MovingBox") && gameObject.name == "Left") { movingBox.GetComponent<MetalBox>().NotLeftEdge(); }
        if ((other.gameObject.tag == "RightEdge" || other.gameObject.tag == "MovingBox") && gameObject.name == "Right") { movingBox.GetComponent<MetalBox>().NotRightEdge(); }

        if (other.gameObject == GameManager.instance.player.gameObject) { movingBox.GetComponent<MetalBox>().CannotSlide(); }
    }
}
