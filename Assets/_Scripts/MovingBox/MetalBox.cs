using UnityEngine;

public class MetalBox : MonoBehaviour
{
    public GameObject player;

    
    public float timeToMove = 10;
    public Transform up;
    public Transform down;
    public Transform left;
    public Transform right;
    public Transform targetTransform;

    [HideInInspector] public int moveDirection = 0;
    [HideInInspector] public bool topEdge = false;
    [HideInInspector] public bool bottomEdge = false;
    [HideInInspector] public bool leftEdge = false;
    [HideInInspector] public bool rightEdge = false;
    [HideInInspector] public bool canSlide;

    private bool attackStarted = false;


    //The movement
    private void Update()
    {
        if (GameManager.instance.player.gameObject.GetComponent<HammerController>().CanAttack && GameManager.instance.player.GetComponent<GearManager>().no)
        {
            attackStarted = false;
        }


        transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, Time.deltaTime * timeToMove);

        if (!attackStarted && Input.GetKeyDown(KeyCode.F) && canSlide && GameManager.instance.player.GetComponent<GearManager>().no)
        {
            attackStarted = true;

            if (!topEdge && moveDirection == 1)
            {
                Invoke("HammerAnimationUp", 0.3f);
            }
            else if (!bottomEdge && moveDirection == 2)
            {
                Invoke("HammerAnimationDown", 0.3f);
            }
            else if (!leftEdge && moveDirection == 3)
            {
                Invoke("HammerAnimationLeft", 0.3f);
            }
            else if (!rightEdge && moveDirection == 4)
            {
                Invoke("HammerAnimationRight", 0.3f);
            }
        }
    }

    //Detects the direction of the movement
    public void UpDir() { moveDirection = 1; }
    public void DownDir() { moveDirection = 2; }
    public void LeftDir() { moveDirection = 3; }
    public void RightDir() { moveDirection = 4; }

    //Functions to see if you've reached and edge
    public void TopEdge() { topEdge = true; }
    public void NotTopEdge() { topEdge = false; }

    public void BottomEdge() { bottomEdge = true; }
    public void NotBottomEdge() { bottomEdge = false; }

    public void LeftEdge() { leftEdge = true; }
    public void NotLeftEdge() { leftEdge = false; }

    public void RightEdge() { rightEdge = true; }
    public void NotRightEdge() { rightEdge = false; }

    public void CanSlide() { canSlide = true; }
    public void CannotSlide() { canSlide = false; }

    // The movement
    private void HammerAnimationUp()
    {
        targetTransform.position = up.position;
    }
    private void HammerAnimationDown()
    {
        targetTransform.position = down.position;
    }
    private void HammerAnimationLeft()
    {
        targetTransform.position = left.position;
    }
    private void HammerAnimationRight()
    {
        targetTransform.position = right.position;
    }

}
