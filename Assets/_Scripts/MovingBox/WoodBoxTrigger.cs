using UnityEngine;

public class WoodBoxTrigger : MonoBehaviour
{
    public GameObject movingBox;
    public GameObject player;

    public Transform hitBoxTransform;


    private void OnTriggerEnter(Collider other)
    {
        //Detects which direction the box should move based on which side the player is on

        if (other.gameObject == GameManager.instance.player.gameObject)
        {
            if (gameObject.name == "Up")
            {
                movingBox.GetComponent<WoodenBox>().DownDir();
            }
            else if (gameObject.name == "Down")
            {
                movingBox.GetComponent<WoodenBox>().UpDir();
            }
            else if (gameObject.name == "Left")
            {
                movingBox.GetComponent<WoodenBox>().RightDir();
            }
            else if (gameObject.name == "Right")
            {
                movingBox.GetComponent<WoodenBox>().LeftDir();
            }
        }
        if (other.gameObject.tag == "MovingBox" && movingBox.GetComponent<SlideBox>().isSliding)
        {
            if (gameObject.name == "Up" && movingBox.GetComponent<WoodenBox>().moveDirection == 1)
            {
                movingBox.GetComponent<SlideBox>().HitAnotherBox();
                hitBoxTransform.position = other.transform.position;
            }
            else if (gameObject.name == "Down" && movingBox.GetComponent<WoodenBox>().moveDirection == 2)
            {
                movingBox.GetComponent<SlideBox>().HitAnotherBox();
                hitBoxTransform.position = other.transform.position;
            }
            else if (gameObject.name == "Left" && movingBox.GetComponent<WoodenBox>().moveDirection == 3)
            {
                movingBox.GetComponent<SlideBox>().HitAnotherBox();
                hitBoxTransform.position = other.transform.position;
            }
            else if (gameObject.name == "Right" && movingBox.GetComponent<WoodenBox>().moveDirection == 4)
            {
                movingBox.GetComponent<SlideBox>().HitAnotherBox();
                hitBoxTransform.position = other.transform.position;
            }

        }


    }
    private void OnTriggerStay(Collider other)
    {
        if ((other.gameObject.tag == "TopEdge" || other.gameObject.tag == "MovingBox") && gameObject.name == "Up") { movingBox.GetComponent<WoodenBox>().TopEdge(); }
        if ((other.gameObject.tag == "BottomEdge" || other.gameObject.tag == "MovingBox") && gameObject.name == "Down") { movingBox.GetComponent<WoodenBox>().BottomEdge(); }
        if ((other.gameObject.tag == "LeftEdge" || other.gameObject.tag == "MovingBox") && gameObject.name == "Left") { movingBox.GetComponent<WoodenBox>().LeftEdge(); }
        if ((other.gameObject.tag == "RightEdge" || other.gameObject.tag == "MovingBox") && gameObject.name == "Right") { movingBox.GetComponent<WoodenBox>().RightEdge(); }


        if (other.gameObject == GameManager.instance.player.gameObject) { movingBox.GetComponent<SlideBox>().CanSlide(); }
    }
    private void OnTriggerExit(Collider other)
    {
        if ((other.gameObject.tag == "TopEdge" || other.gameObject.tag == "MovingBox") && gameObject.name == "Up") {movingBox.GetComponent<WoodenBox>().NotTopEdge();}
        if ((other.gameObject.tag == "BottomEdge" || other.gameObject.tag == "MovingBox") && gameObject.name == "Down") { movingBox.GetComponent<WoodenBox>().NotBottomEdge();}
        if ((other.gameObject.tag == "LeftEdge" || other.gameObject.tag == "MovingBox") && gameObject.name == "Left") {movingBox.GetComponent<WoodenBox>().NotLeftEdge();}
        if ((other.gameObject.tag == "RightEdge" || other.gameObject.tag == "MovingBox") && gameObject.name == "Right") {movingBox.GetComponent<WoodenBox>().NotRightEdge();}

        if (other.gameObject == GameManager.instance.player.gameObject) { movingBox.GetComponent<SlideBox>().CannotSlide(); }
    }
}
